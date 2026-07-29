using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// El panel que se abre al interactuar con el horno. Muestra los sprites
// de los platillos disponibles, y cuando el jugador elige uno le pregunta
// al Horno si se puede preparar. Si faltan ingredientes muestra un
// mensaje de aviso en vez de cerrar el panel.
public class HornoUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelUI; 

    [Header("Slots (uno por cada uno de los 9 platillos)")]
    public SlotRecetaUI[] slotsDeReceta;

    [Header("Mensaje de error")]
    public TMP_Text textoMensaje;
    public float duracionMensaje = 2f;

    private Horno hornoActual;

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

    // Lo llama el Horno cuando el jugador presiona Espacio frente a el
    public void AbrirUI(Horno horno, Receta[] recetas)
    {
        hornoActual = horno;

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
        if (hornoActual == null) return;

        bool sePudoPreparar = hornoActual.IntentarPrepararReceta(receta);

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