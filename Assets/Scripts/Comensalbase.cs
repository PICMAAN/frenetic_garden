using UnityEngine;
using UnityEngine.UIElements;

// Clase padre de los 6 comensales (Pollo, Vaca, Cerdo, Tortuga, Rana, Oveja).
// Se encarga de: buscar y ocupar un asiento libre al aparecer (o despawnear
// si no hay lugar), guardar que platillo pidio, mostrar y ocultar su globo
// de dialogo, correr su tiempo de paciencia, y liberar el asiento cuando se va.
public abstract class ComensalBase : MonoBehaviour
{
    [Header("Tipo")]
    public Tipocomensal tipo;

    [Header("Identificacion")]
    [Tooltip("Nombre que se muestra arriba de la cabeza, editalo aqui para cada comensal")]
    public string nickname = "Comensal";

    [Header("Paciencia")]
    public float tiempoPacienciaSegundos = 60f;

    [Header("Movimiento")]
    public float velocidadDeCaminata = 3f;

    [Header("Referencias")]
    [Tooltip("El globo de dialogo que va arriba de la cabeza de este NPC")]
    public Globodialogonpc globo;

    [Tooltip("La etiqueta de nombre estilo Minecraft, siempre visible")]
    public Nametagnpc etiquetaDeNombre;

    private Receta pedidoActual;
    private bool yaFueAtendido = false;
    private Asiento asientoAsignado;

    protected virtual void Awake()
    {
        ConfigurarTipo();

        if (globo == null)
        {
            globo = GetComponentInChildren<Globodialogonpc>();
        }

        if (etiquetaDeNombre == null)
        {
            etiquetaDeNombre = GetComponentInChildren<Nametagnpc>();
        }

        if (etiquetaDeNombre != null)
        {
            etiquetaDeNombre.MostrarNombre(nickname);
        }
    }

    // Cada hijo (ComensalPollo, ComensalVaca, etc) pone aqui su propio TipoComensal
    protected abstract void ConfigurarTipo();

    // Lo llama el GestorSpawnComensales apenas se crea el NPC.
    // Busca un asiento libre: si encuentra, se sienta y le muestra el pedido.
    // Si no hay ningun asiento libre, se va de inmediato sin pedir nada.
    public void IntentarSentarse(Receta receta)
    {
        Asiento asientoLibre = GestorDeAsientos.Instancia.BuscarAsientoLibre();

        if (asientoLibre == null)
        {
            Destroy(gameObject); // no hay lugar, se va y despawnea
            return;
        }

        asientoAsignado = asientoLibre;
        asientoLibre.Ocupar(this);

        MovimientoNPC movimiento = GetComponent<MovimientoNPC>();
        if (movimiento != null)
        {
            movimiento.IrHaciaPunto(asientoLibre.puntoDeAsiento, velocidadDeCaminata);
        }
        else
        {
            transform.position = asientoLibre.puntoDeAsiento.position;
        }

        AsignarPedido(receta);
    }

    private void AsignarPedido(Receta receta)
    {
        pedidoActual = receta;
        yaFueAtendido = false;

        if (globo != null)
        {
            globo.MostrarPedido(receta, tiempoPacienciaSegundos, OnSeAgotoPaciencia);
        }
    }

    // Se dispara solo si la barra de paciencia del globo llega a cero
    private void OnSeAgotoPaciencia()
    {
        if (yaFueAtendido) return;
        yaFueAtendido = true;
        Irse(false);
    }

    // Lo llama el Asiento cuando el jugador intenta entregarle un platillo.
    // Devuelve true solo si el platillo entregado es el que pidio.
    public bool IntentarEntregar(Receta platilloEntregado)
    {
        if (yaFueAtendido) return false;

        if (platilloEntregado == pedidoActual)
        {
            yaFueAtendido = true;
            if (globo != null) globo.OcultarGlobo();
            Irse(true);
            return true;
        }

        return false;
    }

    private void Irse(bool satisfecho)
    {
        if (asientoAsignado != null)
        {
            asientoAsignado.Liberar();
        }

        Destroy(gameObject);
    }

    public Receta ObtenerPedido()
    {
        return pedidoActual;
    }
}