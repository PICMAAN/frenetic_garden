using UnityEngine;

//Por ahora todos los incredientes se llamaran incrediente 1,2,3... 16
//Cuando se entregren los nombres decada uno de los ingredientes se remplazaran con sus respectivos nombres

[CreateAssetMenu(fileName = "NuevoIngrediente", menuName =
    "FreneticGarden/Ingrediente")]
public class Ingredientes : ScriptableObject
{
    [Header("Identificacion")]
    public int id; // 1 a 16
    public string nombreIngrediente = "Ingrediente1";

    [Header("Visual")]
    public Sprite icono;

    [Header("Cultivo")]
    public float tiempoDeCrecimiento = 10f;
    public GameObject prefebPlantado;
    public GameObject postfebCrecido;
}