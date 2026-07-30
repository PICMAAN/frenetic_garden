using UnityEngine;
using UnityEngine.UIElements;

// Guarda el platillo que el jugador acaba de recoger de un instrumento
// de cocina, hasta que lo entregue en una mesa (Asiento). Solo se puede
// cargar un platillo a la vez, como en cualquier juego de cocina.
// Tu script de jugador puede leer PortadorDePlatillo.Instancia.PlatilloActual
// para saber que trae en las manos, por ejemplo para mostrar su icono
// arriba del personaje, y llamar Asiento.EntregarPlatillo(ese platillo)
// cuando interactue con una mesa.
public class PortadorDePlatillo : MonoBehaviour
{
    public static PortadorDePlatillo Instancia { get; private set; }

    public Receta PlatilloActual { get; private set; }

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
    }

    public bool TienePlatillo()
    {
        return PlatilloActual != null;
    }

    // Lo llama la EstacionDeCocina cuando el jugador recoge el platillo listo
    public bool RecogerPlatillo(Receta receta)
    {
        if (TienePlatillo()) return false; // ya trae uno, no puede cargar dos

        PlatilloActual = receta;
        return true;
    }

    // Lo llama tu script de jugador despues de entregarlo exitosamente en una mesa
    public void SoltarPlatillo()
    {
        PlatilloActual = null;
    }
}