using UnityEngine;

// Se pone en el mismo prefab que ComensalBase. Hace que el NPC camine
// hacia el punto que se le indique (en este caso, su asiento asignado)
// en vez de aparecer ahi de golpe.
public class MovimientoNPC : MonoBehaviour
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