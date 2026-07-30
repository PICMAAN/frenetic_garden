using UnityEngine;
using UnityEngine.UI;

// Un icono seleccionable dentro del menu de cualquier instrumento de
// cocina (horno, tabla de cortar, licuadora, olla). Muestra el sprite
// del platillo y avisa a EstacionUI cuando el jugador lo elige.
public class SlotRecetaUI : MonoBehaviour
{
    public Image imagenPlatillo;
    public Button boton;

    private Receta recetaAsignada;
    private EstacionUI menuPadre;

    public void Configurar(Receta receta, EstacionUI menu)
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