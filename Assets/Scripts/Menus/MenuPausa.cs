using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausa : MonoBehaviour
{
    private bool estaPausado;
    [SerializeField] private GameObject panelPausa;

    private InputSystem_Actions acciones;

    private void Awake()
    {
        acciones = new InputSystem_Actions();
        Time.timeScale = 1f; // Garantiza que el juego arranque a velocidad normal
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    private void OnEnable()
    {
        acciones.Player.Enable();
        acciones.Player.Pausa.performed += Pausa;
    }

    private void OnDisable()
    {
        acciones.Player.Pausa.performed -= Pausa;
        acciones.Player.Disable();
    }

    public void Pausa(InputAction.CallbackContext ctx)
    {
        CambioEstadoPausa();
    }

    public void CambioEstadoPausa()
    {
        estaPausado = !estaPausado;

        if (estaPausado)
        {
            Time.timeScale = 0f;
            if (panelPausa != null) panelPausa.SetActive(true);

            // Le pedimos al MusicManager persistente que pause
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.PausarMusica();
            }
        }
        else
        {
            Time.timeScale = 1f;
            if (panelPausa != null) panelPausa.SetActive(false);

            // Le pedimos al MusicManager persistente que reanude
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.ReanudarMusica();
            }
        }
    }

    // Aseguramos que si destruyes este panel (por cambiar de escena), el tiempo vuelva a 1
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}