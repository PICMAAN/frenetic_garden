using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPausa : MonoBehaviour
{
    //VARIABLES
    private bool estaPausado;
    [SerializeField] private GameObject panelPausa;
    InputSystem_Actions acciones;
    
    void Awake()
    {
        acciones = new InputSystem_Actions();
        Time.timeScale = 1f;           //Obliga a empezar el juego siempre activo despues del cambio de escena que pausa el tiempo.
        panelPausa.SetActive(false);   //Obliga a tener internamente el panelPausa desativado para no dar doble click equilibrando los estados del if de abajo
    }
    
    void OnEnable()
    {
        acciones.Player.Enable();
        
        acciones.Player.Pausa.performed += pausa;
    }
    
    void OnDisable()
    {
        acciones.Player.Pausa.performed -= pausa;
        
        acciones.Player.Disable();
    }
    
    public void pausa(InputAction.CallbackContext ctx)
    {
        cambioEstadoPausa();
    }
    public void cambioEstadoPausa()
    {
        if (estaPausado == true)
        {
            Time.timeScale = 1f;
            panelPausa.SetActive(false);
            estaPausado = false;
        }
        else
        {
            Time.timeScale = 0f;
            panelPausa.SetActive(true);
            estaPausado = true;
        }
    }
    

}
