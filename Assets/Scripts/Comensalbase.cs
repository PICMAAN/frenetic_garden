using UnityEngine;
using UnityEngine.UIElements;

// Se encarga de: guardar que platillo pidio, mostrar y ocultar su globo
// de dialogo, correr su tiempo de paciencia, y avisar a la fila cuando
// se va (satisfecho o no).
public abstract class ComensalBase : MonoBehaviour
{
    [Header("Tipo")]
    public TipoComensal tipo;

    [Header("Identificacion")]
    [Tooltip("Nombre que se muestra arriba de la cabeza, editalo aqui para cada comensal")]
    public string nickname = "Comensal";

    [Header("Paciencia")]
    public float tiempoPacienciaSegundos = 60f;

    [Header("Referencias")]
    [Tooltip("El globo de dialogo que va arriba de la cabeza de este NPC")]
    public GloboDialogoNPC globo;

    [Tooltip("La etiqueta de nombre estilo Minecraft, siempre visible")]
    public NametagNPC etiquetaDeNombre;

    private Receta pedidoActual;
    private bool yaFueAtendido = false;

    protected virtual void Awake()
    {
        ConfigurarTipo();

        if (globo == null)
        {
            globo = GetComponentInChildren<GloboDialogoNPC>();
        }

        if (etiquetaDeNombre == null)
        {
            etiquetaDeNombre = GetComponentInChildren<NametagNPC>();
        }

        if (etiquetaDeNombre != null)
        {
            etiquetaDeNombre.MostrarNombre(nickname);
        }
    }

    // Cada hijo pone aqui su propio tipo de comensal
    protected abstract void ConfigurarTipo();

    // Lo llama el GestorSpawnComensales apenas se crea el NPC, ya sea que
    // caiga directo en la ventanilla o hasta atras de la fila.
    public void AsignarPedido(Receta receta)
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

    // Lo llama la Ventanilla cuando el jugador intenta entregarle un platillo.
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
        FilaComensales.Instancia.NotificarSalida(this);
        Destroy(gameObject);
    }

    public Receta ObtenerPedido()
    {
        return pedidoActual;
    }
}