using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ControladorJugador : MonoBehaviour
{
    //VARIABLES DE MOVIMIENTO BASICO
    [SerializeField] private float speed;
    private float moveX;
    private float moveY;

    //VARIABLES DE DASH
    [SerializeField] private float fuerzaDash;
    [SerializeField] private AnimationClip cLipDash;
    private bool poderHacerDash = true;
    private bool haciendoDash;

    //VARIABLES DE INTERACCION
    [SerializeField] private float radioDeInteraccion = 0.5f;

    //VARIABLES TIPO ESTRUCTURAS
    Rigidbody2D rb2D;
    Animator animator;
    public InputSystem_Actions acciones;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem particulasDash;


    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        acciones = new InputSystem_Actions();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        acciones.Player.Enable();

        acciones.Player.Move.performed += movimientoX;
        acciones.Player.Move.canceled += movimientoX;

        acciones.Player.Move.performed += movimientoY;
        acciones.Player.Move.canceled += movimientoY;

        acciones.Player.Sprint.performed += eventoDash;
        acciones.Player.Sprint.canceled += eventoDash;

        acciones.Player.Interact.performed += eventoInteractuar;
    }

    void OnDisable()
    {
        acciones.Player.Move.performed -= movimientoX;
        acciones.Player.Move.canceled -= movimientoX;

        acciones.Player.Sprint.performed -= eventoDash;
        acciones.Player.Sprint.canceled -= eventoDash;

        acciones.Player.Move.performed -= movimientoY;
        acciones.Player.Move.canceled -= movimientoY;

        acciones.Player.Interact.performed -= eventoInteractuar;

        acciones.Player.Disable();
    }


    void movimientoX(InputAction.CallbackContext ctx)
    {
        moveX = ctx.ReadValue<Vector2>().x;
    }

    void movimientoY(InputAction.CallbackContext ctx)
    {
        moveY = ctx.ReadValue<Vector2>().y;
    }

    void eventoParticulasDash()
    {
        particulasDash.Play();
    }
    void eventoDash(InputAction.CallbackContext ctx)
    {
        if (poderHacerDash == true)
        {
            if (rb2D.linearVelocityX > 0.1f || rb2D.linearVelocityX < -0.1f)
            {
                Debug.Log("Evento daash activado");
                StartCoroutine(movimientoDash());
            }
            else
            {
                Debug.Log("Evento dash activado pero no ejecutado");
                poderHacerDash = false;
                StartCoroutine(cooldownDash());
            }

        }

    }
    IEnumerator movimientoDash()
    {
        poderHacerDash = false;
        haciendoDash = true;
        eventoParticulasDash();

        rb2D.linearVelocity = new Vector2(rb2D.linearVelocityY, 0);
        speed += fuerzaDash;
        yield return new WaitForSeconds(cLipDash.length);
        speed -= fuerzaDash;

        haciendoDash = false;
        StartCoroutine(cooldownDash());
    }
    IEnumerator cooldownDash()
    {
        yield return new WaitForSeconds(2f);
        poderHacerDash = true;
    }

    // Se dispara al presionar Espacio (accion "Interact" del Input System).
    // Busca cualquier IInteractuable cerca del jugador (cultivos, instrumentos
    // de cocina, mesas) y le llama Interactuar(). Si hay varios encimados,
    // solo interactua con el primero que encuentre.
    void eventoInteractuar(InputAction.CallbackContext ctx)
    {
        Collider2D[] cercanos = Physics2D.OverlapCircleAll(transform.position, radioDeInteraccion);

        foreach (Collider2D col in cercanos)
        {
            IInteractuable interactuable = col.GetComponent<IInteractuable>();

            if (interactuable != null)
            {
                interactuable.Interactuar();
                break;
            }
        }
    }



    void flip()
    {
        if (rb2D.linearVelocity.x > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb2D.linearVelocity.x < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    void movimientoDiagonal()
    {
        if (rb2D.linearVelocityX > 0.1f && rb2D.linearVelocityY > 0.1f)
        {
            Debug.Log("Movimiento Diagonal derecho-superior");
            //FALTAN LOS ANIMATORS
        }
        else if (rb2D.linearVelocityX > 0.1f && rb2D.linearVelocityY < -0.1f)
        {
            Debug.Log("Movimiento Diagonal derecho-inferior");
        }

    }
    private void FixedUpdate()
    {
        rb2D.linearVelocityX = moveX * speed;
        animator.SetFloat("Vx", Mathf.Abs(rb2D.linearVelocityX));
        flip();

        if (haciendoDash == false)
        {
            rb2D.linearVelocityY = moveY * speed;
            animator.SetFloat("Vy", rb2D.linearVelocityY);
            flip();
        }


        movimientoDiagonal();
        flip();
    }

    // Solo para ver en el editor que tan grande es el radio de interaccion
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeInteraccion);
    }

}