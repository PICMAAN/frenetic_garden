using UnityEngine;

// Define uno de los 9 platillos del juego.
// ingredientesFijos: los que siempre se necesitan todos.
// ingredientesAlternativos: se necesita al menos UNO cualquiera de esta
// lista (se usa solo en "Agua de frutas", donde puede ser fresa, sandia
// o melon). Para las demas recetas deja este arreglo vacio.
[CreateAssetMenu(fileName = "NuevaReceta", menuName = "FreneticGarden/Receta")]
public class Receta : ScriptableObject
{
    [Header("Identificacion")]
    public string nombrePlatillo;
    public Sprite iconoPlatillo;
    public GameObject prefabPlatilloListo;

    [Header("Ingredientes que siempre se necesitan")]
    public Ingrediente[] ingredientesFijos;

    [Header("Ingredientes alternativos (se necesita al menos UNO, dejar vacio si no aplica)")]
    public Ingrediente[] ingredientesAlternativos;
}