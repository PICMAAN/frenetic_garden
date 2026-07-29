using System;
using System.Collections.Generic;
using UnityEngine;

// Maneja las cantidades de cada uno de los 16 ingredientes.
// Es un singleton para que cualquier script (parcela, estacion, UI)
// pueda acceder desde InventarioManager.Instancia
public class InventarioManager : MonoBehaviour
{
    public static InventarioManager Instancia { get; private set; }

    [Header("Configuracion")]
    [Tooltip("Arrastra aqui los 14 ScriptableObjects de Ingrediente, en cualquier orden")]
    public List<Ingrediente> ingredientesDisponibles;

    // Diccionario interno: ingrediente -> cantidad
    private Dictionary<Ingrediente, int> cantidades = new Dictionary<Ingrediente, int>();

    // Evento que la UI escucha para refrescarse cada vez que cambia el inventario
    public event Action OnInventarioActualizado;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;

        foreach (Ingrediente ing in ingredientesDisponibles)
        {
            if (ing != null && !cantidades.ContainsKey(ing))
            {
                cantidades.Add(ing, 0);
            }
        }
    }

    // Se llama cuando el jugador cosecha una parcela
    public void AgregarIngrediente(Ingrediente ingrediente, int cantidad = 1)
    {
        if (ingrediente == null) return;

        if (!cantidades.ContainsKey(ingrediente))
        {
            cantidades.Add(ingrediente, 0);
        }

        cantidades[ingrediente] += cantidad;
        OnInventarioActualizado?.Invoke();
    }

    // Revisa si hay suficiente cantidad de un ingrediente.
    // Si el ingrediente esta marcado como infinito (el Agua), siempre es true.
    public bool TieneSuficiente(Ingrediente ingrediente, int cantidadRequerida = 1)
    {
        if (ingrediente == null) return false;
        if (ingrediente.esInfinito) return true;
        return cantidades.ContainsKey(ingrediente) && cantidades[ingrediente] >= cantidadRequerida;
    }

    // Resta ingredientes del inventario, por ejemplo al preparar un platillo.
    // Si el ingrediente es infinito (el Agua), no se resta nada.
    public bool ConsumirIngrediente(Ingrediente ingrediente, int cantidad = 1)
    {
        if (ingrediente != null && ingrediente.esInfinito) return true;
        if (!TieneSuficiente(ingrediente, cantidad)) return false;

        cantidades[ingrediente] -= cantidad;
        OnInventarioActualizado?.Invoke();
        return true;
    }

    // Devuelve la cantidad actual de un ingrediente, util para la UI
    public int ObtenerCantidad(Ingrediente ingrediente)
    {
        if (ingrediente == null) return 0;
        return cantidades.ContainsKey(ingrediente) ? cantidades[ingrediente] : 0;
    }

    // Devuelve todo el inventario, util para dibujar los 16 slots en la UI
    public Dictionary<Ingrediente, int> ObtenerInventarioCompleto()
    {
        return cantidades;
    }
}