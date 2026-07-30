using UnityEngine;

// Crea un asset por cada uno desde el menu de Unity.
[CreateAssetMenu(fileName = "NuevoIngrediente", menuName = "FreneticGarden/Ingrediente")]
public class Ingrediente : ScriptableObject
{
    [Header("Identificacion")]
    public string nombreIngrediente;

    [Header("Visual")]
    public Sprite icono;

    [Header("Especial")]
    [Tooltip("Marca esto SOLO en el ingrediente Agua. Hace que siempre haya cantidad infinita, nunca se resta del inventario")]
    public bool esInfinito = false;

    [Header("Cultivo (dejar en 0 si es Agua, no tiene cultivo)")]
    public float tiempoDeCrecimiento = 10f; // segundos hasta poder cosechar
}