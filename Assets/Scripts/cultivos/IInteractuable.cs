// Para todo lo que el jugador pueda interactuar con Espacio:
// cultivos, el horno, etc. Tu script de jugador ya existente solo necesita
// detectar el objeto mas cercano/en contacto, revisar si implementa esta
// interfaz, y llamar a Interactuar() cuando se presione la tecla.
public interface IInteractuable
{
    void Interactuar();
}