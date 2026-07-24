using UnityEngine;
using UnityEngine.InputSystem;

public class CultivosIngredientes : MonoBehaviour
{
    //VARIABLE DE INTERACCION
    private bool jugadorEnRango;
    
    
    public InputSystem_Actions acciones;

    void Awake()
    {
        acciones = new InputSystem_Actions();
    }
    
    
    private void OnEnable()
    {
        acciones.Player.Enable();
        
        acciones.Player.Interact.performed += eventoInteraccion;
        acciones.Player.Interact.canceled += eventoInteraccion;
    }

    void OnDisable()
    {
        acciones.Player.Interact.performed -= eventoInteraccion;
        acciones.Player.Interact.canceled -= eventoInteraccion;

        acciones.Player.Disable();
    }
    
    
    
    public void eventoInteraccion(InputAction.CallbackContext ctx)
    {
        if (jugadorEnRango == true)
        {
            Debug.Log("Evento Interaccion ACTIVADO");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            Debug.Log("Jugador EnRango");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("Jugador Saliendo de Rango");
        }
    }
}
