using UnityEngine;
using UnityEngine.UIElements;

// Se pone en el objeto de la ventanilla. El script del jugador (que
// todavia no tenemos, se hace cuando definas el movimiento/interaccion)
// debe llamar a EntregarPlatillo() pasando el platillo que trae en las
// manos cuando el jugador presione el boton de interactuar frente a esto.
public class VentanillaEntrega : MonoBehaviour
{
    // Devuelve true si el comensal de la ventanilla acepto el platillo
    public bool EntregarPlatillo(Receta platilloEnManos)
    {
        Comensalbase comensalActual = FilaComensales.Instancia.ObtenerComensalEnVentanilla();

        if (comensalActual == null)
        {
            return false; // no hay nadie en la ventanilla
        }

        return comensalActual.IntentarEntregar(platilloEnManos);
    }
}