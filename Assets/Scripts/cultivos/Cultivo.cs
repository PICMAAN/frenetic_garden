using System.Collections;
using UnityEngine;

// Va en cada espacio de cultivo ya colocado en la escena (uno por cada
// uno de los 13 ingredientes cultivables, el Agua no lleva este script
// porque es infinita y no se cultiva).
// ese tiempo de espera es el mismo tiempoDeCrecimiento, funciona como
// el cooldown antes de poder volver a cosechar.
public class Cultivo : MonoBehaviour, IInteractuable
{
    [Header("Que produce este cultivo")]
    public Ingrediente ingredienteQueProduce;

    [Header("Tiempo de crecimiento / cooldown (segundos)")]
    public float tiempoDeCrecimiento = 10f;

    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteCreciendo;
    public Sprite spriteListoParaCosechar;

    private bool listoParaCosechar = false;

    private void Start()
    {
        EmpezarCrecimiento();
    }

    private void EmpezarCrecimiento()
    {
        listoParaCosechar = false;

        if (spriteRenderer != null && spriteCreciendo != null)
        {
            spriteRenderer.sprite = spriteCreciendo;
        }

        StartCoroutine(EsperarYQuedarListo());
    }

    private IEnumerator EsperarYQuedarListo()
    {
        yield return new WaitForSeconds(tiempoDeCrecimiento);

        listoParaCosechar = true;

        if (spriteRenderer != null && spriteListoParaCosechar != null)
        {
            spriteRenderer.sprite = spriteListoParaCosechar;
        }
    }

    // Lo llama el script del jugador cuando presiona Espacio cerca de este cultivo
    public void Interactuar()
    {
        if (!listoParaCosechar) return;

        InventarioManager.Instancia.AgregarIngrediente(ingredienteQueProduce, 1);

        // vuelve a arrancar el ciclo, esto hace de cooldown antes de la siguiente cosecha
        EmpezarCrecimiento();
    }
}