using System.Collections.Generic;
using UnityEngine;

// El puesto 0 de puntosDeFila es la ventanilla,
// los demas son los lugares donde esperan formados. Cada vez que alguien
// entra o sale, todos los que siguen avanzan un lugar.
public class FilaComensales : MonoBehaviour
{
    public static FilaComensales Instancia { get; private set; }

    [Header("Configuracion")]
    [Tooltip("Posicion 0 = ventanilla. El resto son los lugares de la fila en orden")]
    public Transform[] puntosDeFila;

    [Tooltip("Que tan rapido se mueve el comensal hacia su nuevo lugar en la fila")]
    public float velocidadDeAvance = 3f;

    private List<Comensalbase> cola = new List<Comensalbase>();

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
    }

    // Lo llama el GestorSpawnComensales cuando aparece un comensal nuevo
    public void Encolar(Comensalbase nuevoComensal)
    {
        cola.Add(nuevoComensal);
        AcomodarPosiciones();
    }

    // Lo llama ComensalBase cuando se va, satisfecho o no
    public void NotificarSalida(Comensalbase comensalQueSeVa)
    {
        if (cola.Contains(comensalQueSeVa))
        {
            cola.Remove(comensalQueSeVa);
        }
        AcomodarPosiciones();
    }

    // El comensal que esta justo en la ventanilla, el unico que se puede atender
    public Comensalbase ObtenerComensalEnVentanilla()
    {
        return cola.Count > 0 ? cola[0] : null;
    }

    // Reasigna a cada comensal el punto de fila que le toca segun su lugar en la lista.
    // El movimiento real hacia esa posicion se hace en Update de cada comensal,
    // aqui solo se les dice a donde tienen que ir.
    private void AcomodarPosiciones()
    {
        for (int i = 0; i < cola.Count; i++)
        {
            if (i >= puntosDeFila.Length) break;

            MovimientoEnFila movimiento = cola[i].GetComponent<MovimientoEnFila>();
            if (movimiento != null)
            {
                movimiento.IrHaciaPunto(puntosDeFila[i], velocidadDeAvance);
            }
            else
            {
                cola[i].transform.position = puntosDeFila[i].position;
            }
        }
    }
}