using System.Collections.Generic;
using UnityEngine;

// Singleton que conoce todos los asientos del restaurante.
// Se encarga unicamente de decir cual esta libre, nada mas.
public class GestorDeAsientos : MonoBehaviour
{
    public static GestorDeAsientos Instancia { get; private set; }

    [Tooltip("Arrastra aqui todos los Asientos (mesas) del restaurante")]
    public List<Asiento> asientos = new List<Asiento>();

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
    }

    // Devuelve el primer asiento libre que encuentre, o null si todos estan ocupados
    public Asiento BuscarAsientoLibre()
    {
        foreach (Asiento asiento in asientos)
        {
            if (!asiento.estaOcupado)
            {
                return asiento;
            }
        }

        return null;
    }
}