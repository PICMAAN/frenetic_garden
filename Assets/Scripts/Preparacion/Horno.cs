using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Interactua con el horno y se abre una miniUI con los 9 platillos.
// Al elegir uno, revisa si hay suficientes ingredientes en el inventario:
// si si, los resta y genera el platillo, si no, HornoUI muestra el
// mensaje de que faltan ingredientes y no deja continuar.
public class Horno : MonoBehaviour, IInteractuable
{
    [Header("Recetas disponibles (las 9 del juego)")]
    public Receta[] recetasDisponibles;

    [Header("UI")]
    public HornoUI ui;

    [Header("Punto de entrega")]
    public Transform puntoDeSalida; // donde aparece el platillo terminado

    // Lo llama el jugador al presionar Espacio frente al horno
    public void Interactuar()
    {
        if (ui != null)
        {
            ui.AbrirUI(this, recetasDisponibles);
        }
    }

    // Lo llama HornoUI cuando el jugador selecciona un platillo del menu.
    // Devuelve true si se pudo preparar, false si faltaron ingredientes.
    public bool IntentarPrepararReceta(Receta receta)
    {
        if (receta == null) return false;

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
                return false; // no tiene ninguno de los alternativos posibles
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

        GenerarPlatillo(receta);
        return true;
    }

    // Cuenta cuantas veces se repite cada ingrediente en la lista,
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

    private void GenerarPlatillo(Receta receta)
    {
        if (receta.prefabPlatilloListo != null && puntoDeSalida != null)
        {
            Instantiate(receta.prefabPlatilloListo, puntoDeSalida.position, Quaternion.identity);
        }

        Debug.Log("Platillo listo: " + receta.nombrePlatillo);
    }
}