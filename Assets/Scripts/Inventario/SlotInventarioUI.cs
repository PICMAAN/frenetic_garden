using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Representa un slot visual: icono + numero de cantidad
// Se coloca en un prefab de UI con una Image y un TMP_Text o Text
public class SlotInventarioUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image imagenIcono;
    public TMP_Text textoCantidad; // si usas TextMeshPro cambia el tipo a TMP_Text

    private Ingrediente ingredienteAsignado;

    public void Configurar(Ingrediente ingrediente, int cantidad)
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
            textoCantidad.text = ingrediente.esInfinito ? "\u221E" : cantidad.ToString();
        }
    }

    public Ingrediente ObtenerIngrediente()
    {
        return ingredienteAsignado;
    }
}