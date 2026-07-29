using UnityEngine;
using UnityEngine.UI;

// Un icono seleccionable dentro del menu del horno. Muestra el sprite
// del platillo y avisa a HornoUI cuando el jugador lo elige.
public class SlotRecetaUI : MonoBehaviour
{
    public Image imagenPlatillo;
    public Button boton;

    private Receta recetaAsignada;
    private HornoUI menuPadre;

    public void Configurar(Receta receta, HornoUI menu)
    {
        gameObject.SetActive(true);
        recetaAsignada = receta;
        menuPadre = menu;

        if (imagenPlatillo != null)
        {
            imagenPlatillo.sprite = receta.iconoPlatillo;
        }

        if (boton != null)
        {
            boton.onClick.RemoveAllListeners();
            boton.onClick.AddListener(() => menuPadre.SeleccionarReceta(recetaAsignada));
        }
    }
}