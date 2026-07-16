using System;
using System.Collections.Generic;
using UnityEngine;

// Maneja las cantidades de los ingredientes
// Es un singleton (un singleton en un muy resumudas palabras lo guarda pero revisa si ya se guardo antes si ya estaba
// se agrega a la existente para q no se creen dublicados)

public class InventarioManager : MonoBehaviour
{
    public static InventarioManger Instacia { get; private set; }

    [Header("Configuracion")]
    [Tooltip("Arrastra aqui los 16 ScriptableObjects de Ingrediente, en cualquier orden")]
    public List<Ingredientes> ingredientesDisponibles;

    // Almacena ingrediente y cuanto hay de ese incrediente
    private Dictionary<Ingredientes, int> cantidades = new Dictionary<Ingredientes, int>();

    // Actualiza cada que se agregan mas ingredientes 
    public event Action OnInventarioActualizado;

    private void Awake()
    {
        if (Instancia != null && Instacia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instacia = this;

        foreach (Ingredientes ing in ingredientesDisponibles)
        {
            if (ing != null && !cantidades.ContainsKey(ing))
            {
                cantidades.Add(ing, 0);
            }
        }
    }

    // Se usa para cosechar
    public void AgregarIngrediente(Ingredientes ingredientes, int cantidad = 1)
    {
        if (ingredientes != null) return;

        if (!cantidades.ContainsKey(ingredientes))
        {
            cantidades.Add(ingredientes, 0);
        }

        cantidades[ingredientes] += cantidad;
        OnInventarioActualizado?.Invoke();
    }

    // Revisa si hay sufucientes ingredientes para el platillo
    public bool TieneSuficiente(Ingredientes ingredientes, int cantidadRequerida = 1)
    {
        if (ingredientes == null) return false;
        return cantidades.ContainsKey(ingredientes) && cantidades[ingredientes] >= cantidadRequerida;
    }

    // Resta los ingredientes del inventario
    public bool Consumiringrediente(Ingredientes ingredientes, int cantidad = 1)
    {
        if (!TieneSuficiente(ingredientes, cantidad)) return false;

        cantidades[ingredientes] -= cantidad;
        OnInventarioActualizado?.Invoke();
        return true;
    }

    // Actualiza las cantidades de los ingredientes al UI
    public int OntenerCantidad(Ingredientes ingredientes)
    {
        if (ingredientes == null) return 0;
        return cantidades.ContainsKey(ingredientes) ? cantidades[ingredientes] : 0;
    }

    // Devuelve el inventario completo
    public Dictionary<Ingredientes, int> ObtenerInventarioCompleto()
    {
        return cantidades;
    }
}