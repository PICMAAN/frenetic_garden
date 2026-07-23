using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// Va creando comensales al azar entre los 6 tipos, cada cierto tiempo
// random, y les asigna un platillo random que van a pedir.
// Los manda directo a la fila (FilaComensales se encarga de acomodarlos).
public class GestorSpawnComensales : MonoBehaviour
{
    [Header("Prefabs de los 6 comensales")]
    public GameObject[] prefabsComensales; // arrastra los 6 prefabs (Pollo, Vaca, Cerdo, Tortuga, Rana, Oveja)

    [Header("Recetas que pueden pedir")]
    public Receta[] recetasPosibles; // todas las recetas validas del juego

    [Header("Punto donde aparecen")]
    public Transform puntoDeSpawn; // normalmente el ultimo lugar de la fila

    [Header("Tiempos entre apariciones")]
    public float tiempoMinEntreSpawns = 5f;
    public float tiempoMaxEntreSpawns = 15f;

    private void Start()
    {
        StartCoroutine(BucleDeSpawns());
    }

    private IEnumerator BucleDeSpawns()
    {
        while (true)
        {
            float espera = Random.Range(tiempoMinEntreSpawns, tiempoMaxEntreSpawns);
            yield return new WaitForSeconds(espera);

            CrearComensalRandom();
        }
    }

    private void CrearComensalRandom()
    {
        if (prefabsComensales.Length == 0 || recetasPosibles.Length == 0) return;

        GameObject prefabElegido = prefabsComensales[Random.Range(0, prefabsComensales.Length)];
        GameObject instancia = Instantiate(prefabElegido, puntoDeSpawn.position, Quaternion.identity);

        Comensalbase comensal = instancia.GetComponent<Comensalbase>();
        if (comensal == null) return;

        Receta recetaElegida = recetasPosibles[Random.Range(0, recetasPosibles.Length)];
        comensal.AsignarPedido(recetaElegida);

        FilaComensales.Instancia.Encolar(comensal);
    }
}