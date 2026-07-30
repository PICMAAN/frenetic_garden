using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// El panel que se abre al interactuar con CUALQUIER instrumento de
// cocina (Horno, Tabla de Cortar, Licuadora, Olla). Puedes usar la
// misma miniUI para los 4 (un solo Canvas/panel en tu HUD).
public class EstacionUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelUI; // el contenedor que se prende/apaga

    [Header("Slots (tantos como el maximo de recetas que muestres a la vez)")]
    public SlotRecetaUI[] slotsDeReceta;

    [Header("Mensaje de error")]
    public TMP_Text textoMensaje;
    public float duracionMensaje = 2f;

    private EstacionDeCocina estacionActual;

    private void Awake()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(false);
        }

        if (textoMensaje != null)
        {
            textoMensaje.gameObject.SetActive(false);
        }
    }

    // Lo llama la EstacionDeCocina cuando el jugador presiona Espacio frente a ella
    public void AbrirUI(EstacionDeCocina estacion, Receta[] recetas)
    {
        estacionActual = estacion;

        if (panelUI != null)
        {
            panelUI.SetActive(true);
        }

        ConfigurarSlots(recetas);
    }

    public void CerrarUI()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(false);
        }
    }

    private void ConfigurarSlots(Receta[] recetas)
    {
        for (int i = 0; i < slotsDeReceta.Length; i++)
        {
            if (recetas != null && i < recetas.Length && recetas[i] != null)
            {
                slotsDeReceta[i].Configurar(recetas[i], this);
            }
            else
            {
                slotsDeReceta[i].gameObject.SetActive(false);
            }
        }
    }

    // Lo llama SlotRecetaUI cuando el jugador selecciona ese platillo
    public void SeleccionarReceta(Receta receta)
    {
        if (estacionActual == null) return;

        bool sePudoPreparar = estacionActual.IntentarPrepararReceta(receta);

        if (sePudoPreparar)
        {
            CerrarUI();
        }
        else
        {
            MostrarMensaje("No tienes los ingredientes necesarios");
        }
    }

    private void MostrarMensaje(string mensaje)
    {
        if (textoMensaje == null) return;

        StopAllCoroutines();
        StartCoroutine(MostrarMensajeTemporal(mensaje));
    }

    private IEnumerator MostrarMensajeTemporal(string mensaje)
    {
        textoMensaje.text = mensaje;
        textoMensaje.gameObject.SetActive(true);

        yield return new WaitForSeconds(duracionMensaje);

        textoMensaje.gameObject.SetActive(false);
    }
}