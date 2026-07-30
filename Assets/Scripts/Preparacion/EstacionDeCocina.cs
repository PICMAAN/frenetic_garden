using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script generico para CUALQUIER instrumento de cocina de la isla:
// Horno, Tabla de Cortar, Licuadora u Olla. Los 4 usan exactamente la
// misma logica, lo unico que cambia entre uno y otro es que recetas le
// asignas en el Inspector:
//   - Horno: solo Pan de platano
//   - Tabla de Cortar: solo Brocheta de frutas
//   - Licuadora: solo Agua de frutas variada
//   - Olla: el resto de los platillos
//
// Flujo: el jugador interactua (Espacio) y se abre la miniUI. Al elegir
// una receta, si hay ingredientes suficientes se restan del inventario
// y arranca un tiempo de espera (tiempoDePreparacion) durante el cual
// se reproduce la animacion de "funcionando". Mientras tanto el
// instrumento esta ocupado y no se puede volver a abrir la UI.
// Cuando el tiempo termina, el jugador tiene que interactuar UNA VEZ
// MAS para recoger el platillo: ahi se apaga la animacion y el platillo
// pasa a las manos del jugador (PortadorDePlatillo), listo para
// entregarlo al comensal.
public class EstacionDeCocina : MonoBehaviour, IInteractuable
{
    [Header("Recetas que produce ESTE instrumento (no todas las 9)")]
    public Receta[] recetasDisponibles;

    [Header("UI")]
    public EstacionUI ui;

    [Header("Tiempo de preparacion (segundos)")]
    public float tiempoDePreparacion = 5f;

    [Header("Animacion")]
    public Animator animador;
    [Tooltip("Nombre del parametro bool en el Animator Controller que prende/apaga la animacion de funcionando")]
    public string nombreParametroFuncionando = "EstaFuncionando";

    [Header("Punto de entrega")]
    public Transform puntoDeSalida; 

    private bool estaPreparando = false;
    private bool platilloListoParaRecoger = false;
    private Receta recetaListaParaRecoger = null;

    // Lo llama el jugador al presionar Espacio frente a este instrumento
    public void Interactuar()
    {
        if (estaPreparando) return; // esta ocupado, no hace nada todavia

        if (platilloListoParaRecoger)
        {
            RecogerPlatillo();
            return;
        }

        if (ui != null)
        {
            ui.AbrirUI(this, recetasDisponibles);
        }
    }

    // Lo llama EstacionUI cuando el jugador selecciona un platillo del menu.
    // Devuelve true si se pudo empezar a preparar, false si faltaron ingredientes.
    public bool IntentarPrepararReceta(Receta receta)
    {
        if (receta == null || estaPreparando || platilloListoParaRecoger) return false;

        Dictionary<Ingrediente, int> requeridosFijos = ContarPorIngrediente(receta.ingredientesFijos);

        foreach (KeyValuePair<Ingrediente, int> par in requeridosFijos)
        {
            if (!InventarioManager.Instancia.TieneSuficiente(par.Key, par.Value))
            {
                return false;
            }
        }

        Ingrediente alternativoElegido = null;

        if (receta.ingredientesAlternativos != null && receta.ingredientesAlternativos.Length > 0)
        {
            foreach (Ingrediente alternativo in receta.ingredientesAlternativos)
            {
                if (InventarioManager.Instancia.TieneSuficiente(alternativo, 1))
                {
                    alternativoElegido = alternativo;
                    break;
                }
            }

            if (alternativoElegido == null)
            {
                return false;
            }
        }

        // ya se confirmo que hay de todo, ahora si se resta del inventario
        foreach (KeyValuePair<Ingrediente, int> par in requeridosFijos)
        {
            InventarioManager.Instancia.ConsumirIngrediente(par.Key, par.Value);
        }

        if (alternativoElegido != null)
        {
            InventarioManager.Instancia.ConsumirIngrediente(alternativoElegido, 1);
        }

        StartCoroutine(PrepararConTiempo(receta));
        return true;
    }

    private IEnumerator PrepararConTiempo(Receta receta)
    {
        estaPreparando = true;

        if (animador != null)
        {
            animador.SetBool(nombreParametroFuncionando, true);
        }

        yield return new WaitForSeconds(tiempoDePreparacion);

        if (animador != null)
        {
            animador.SetBool(nombreParametroFuncionando, false);
        }

        estaPreparando = false;
        platilloListoParaRecoger = true;
        recetaListaParaRecoger = receta;
    }

    // Se llama cuando el jugador interactua otra vez ya con el platillo listo
    private void RecogerPlatillo()
    {
        if (recetaListaParaRecoger == null) return;

        if (PortadorDePlatillo.Instancia != null)
        {
            PortadorDePlatillo.Instancia.RecogerPlatillo(recetaListaParaRecoger);
        }

        if (recetaListaParaRecoger.prefabPlatilloListo != null && puntoDeSalida != null)
        {
            Instantiate(recetaListaParaRecoger.prefabPlatilloListo, puntoDeSalida.position, Quaternion.identity);
        }

        Debug.Log("Recogiste: " + recetaListaParaRecoger.nombrePlatillo);

        platilloListoParaRecoger = false;
        recetaListaParaRecoger = null;
    }

    // Cuenta cuantas veces se repite cada ingrediente en la lista de fijos,
    // por si alguna receta llegara a pedir 2 del mismo ingrediente
    private Dictionary<Ingrediente, int> ContarPorIngrediente(Ingrediente[] lista)
    {
        Dictionary<Ingrediente, int> conteo = new Dictionary<Ingrediente, int>();

        if (lista == null) return conteo;

        foreach (Ingrediente ing in lista)
        {
            if (ing == null) continue;

            if (!conteo.ContainsKey(ing))
            {
                conteo[ing] = 0;
            }

            conteo[ing]++;
        }

        return conteo;
    }
}