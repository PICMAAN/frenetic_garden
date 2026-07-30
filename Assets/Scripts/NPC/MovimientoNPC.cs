using System;
using UnityEngine;

// Se pone en el mismo prefab que ComensalBase. Hace que el NPC camine
// hacia el punto que se le indique (su asiento asignado) en vez de
// aparecer ahi de golpe. Mientras camina le avisa a ComensalBase para
// que muestre el sprite de caminando con el flip segun la direccion,
// y cuando llega avisa con un callback (por ejemplo para sentarse).
public class MovimientoNPC : MonoBehaviour
{
    [Tooltip("Que tan cerca del destino se considera que ya llego")]
    public float distanciaDeLlegada = 0.05f;

    private Transform destinoActual;
    private float velocidadActual = 3f;
    private Action alLlegar;
    private ComensalBase comensal;

    private void Awake()
    {
        comensal = GetComponent<ComensalBase>();
    }

    // callbackAlLlegar es opcional, se llama una sola vez al llegar al destino
    public void IrHaciaPunto(Transform nuevoDestino, float velocidad, Action callbackAlLlegar = null)
    {
        destinoActual = nuevoDestino;
        velocidadActual = velocidad;
        alLlegar = callbackAlLlegar;
    }

    private void Update()
    {
        if (destinoActual == null) return;

        Vector3 posicionAntes = transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            destinoActual.position,
            velocidadActual * Time.deltaTime
        );

        float direccionX = transform.position.x - posicionAntes.x;

        if (comensal != null)
        {
            comensal.MostrarCaminando(direccionX);
        }

        if (Vector3.Distance(transform.position, destinoActual.position) <= distanciaDeLlegada)
        {
            Action callback = alLlegar;

            destinoActual = null;
            alLlegar = null;

            callback?.Invoke();
        }
    }
}