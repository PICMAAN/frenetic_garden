using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// Va creando comensales al azar entre los 6 tipos, cada cierto tiempo
// Cada comensal busca su propio asiento libre al aparecer
public class GestorSpawnComensales : MonoBehaviour
{
    [Header("Prefabs de los 6 comensales")]
    public GameObject[] prefabsComensales; // arrastra los 6 prefabs (Pollo, Vaca, Cerdo, Tortuga, Rana, Oveja)

    [Header("Recetas que pueden pedir")]
    public Receta[] recetasPosibles; // todas las recetas validas del juego

    [Header("Punto donde aparecen")]
    public Transform puntoDeSpawn; // un punto cualquiera fuera de las mesas, como la entrada

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

        ComensalBase comensal = instancia.GetComponent<ComensalBase>();
        if (comensal == null) return;

        Receta recetaElegida = recetasPosibles[Random.Range(0, recetasPosibles.Length)];
        comensal.IntentarSentarse(recetaElegida);
    }
}