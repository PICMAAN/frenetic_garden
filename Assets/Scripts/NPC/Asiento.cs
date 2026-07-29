using UnityEngine;
using UnityEngine.UIElements;

// Representa un lugar donde un comensal se puede sentar (una mesa,
// o un lugar especifico dentro de una mesa). El jugador tambien usa
// este script para entregarle el platillo a quien este sentado aqui.
public class Asiento : MonoBehaviour
{
    [Tooltip("Punto exacto donde se para/sienta el NPC. Puede ser este mismo objeto")]
    public Transform puntoDeAsiento;

    [HideInInspector] public bool estaOcupado = false;
    [HideInInspector] public ComensalBase comensalActual;

    private void Awake()
    {
        if (puntoDeAsiento == null)
        {
            puntoDeAsiento = transform;
        }
    }

    public void Ocupar(ComensalBase comensal)
    {
        estaOcupado = true;
        comensalActual = comensal;
    }

    public void Liberar()
    {
        estaOcupado = false;
        comensalActual = null;
    }

    // Lo llama el jugador (cuando definamos su script de interaccion)
    // para entregarle el platillo a quien esta sentado en este asiento.
    public bool EntregarPlatillo(Receta platilloEnManos)
    {
        if (!estaOcupado || comensalActual == null) return false;
        return comensalActual.IntentarEntregar(platilloEnManos);
    }
}