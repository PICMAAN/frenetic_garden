using UnityEngine;

// Se pone en el mismo prefab que ComensalBase. Cada vez que la fila
// le asigna un nuevo punto (porque alguien se fue de adelante)
public class MovimientoEnFila : MonoBehaviour
{
    private Transform destinoActual;
    private float velocidadActual = 3f;

    public void IrHaciaPunto(Transform nuevoDestino, float velocidad)
    {
        destinoActual = nuevoDestino;
        velocidadActual = velocidad;
    }

    private void Update()
    {
        if (destinoActual == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            destinoActual.position,
            velocidadActual * Time.deltaTime
        );
    }
}