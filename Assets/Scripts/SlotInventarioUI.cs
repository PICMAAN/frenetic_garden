using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Se coloca en un prefab de UI con una Image y un TMP_Text
public class SlotInventarioUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image imagenIcono;
    public TMP_Text textoCantidad;

    private Ingredientes ingredienteAsignado;

    public void Configurar(Ingredientes ingrediente, int cantidad)
    {
        ingredienteAsignado = ingrediente;

        if (ingrediente == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        if (imagenIcono != null)
        {
            imagenIcono.sprite = ingrediente.icono;
        }

        if (textoCantidad != null)
        {
            textoCantidad.text = cantidad.ToString();
        }
    }

    public Ingredientes ObtenerIngrediente()
    {
        return ingredienteAsignado;
    }
}