using System.Collections.Generic;
using UnityEngine;

// Dibuja en pantalla los slots de los 16 ingredientes.
<<<<<<< Updated upstream
// Se suscribe al InventarioManager para actualizarce automaticamente.
=======
// Se suscribe al InventarioManager para refrescarse automaticamente.
>>>>>>> Stashed changes
public class InventarioUI : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject prefabSlot;
<<<<<<< Updated upstream
    public Transform contenedorSlots; 
=======
    public Transform contenedorSlots; // el objeto con el Grid Layout Group o Horizontal Layout Group
>>>>>>> Stashed changes

    private List<SlotInventarioUI> slotsCreados = new List<SlotInventarioUI>();

    private void OnEnable()
    {
        if (InventarioManager.Instancia != null)
        {
            InventarioManager.Instancia.OnInventarioActualizado += RefrescarUI;
        }
    }

    private void OnDisable()
    {
        if (InventarioManager.Instancia != null)
        {
            InventarioManager.Instancia.OnInventarioActualizado -= RefrescarUI;
        }
    }

    private void Start()
    {
        CrearSlots();
        RefrescarUI();
    }

    // Crea un slot por cada ingrediente configurado en el InventarioManager
    private void CrearSlots()
    {
        foreach (Transform hijo in contenedorSlots)
        {
            Destroy(hijo.gameObject);
        }
        slotsCreados.Clear();

        foreach (Ingredientes ing in InventarioManager.Instancia.ingredientesDisponibles)
        {
            if (ing == null) continue;

            GameObject nuevoSlotObj = Instantiate(prefabSlot, contenedorSlots);
            SlotInventarioUI slot = nuevoSlotObj.GetComponent<SlotInventarioUI>();
            slotsCreados.Add(slot);
        }
    }

    // Actualiza todos los slots con las cantidades actuales
    private void RefrescarUI()
    {
        List<Ingredientes> lista = InventarioManager.Instancia.ingredientesDisponibles;

        for (int i = 0; i < slotsCreados.Count && i < lista.Count; i++)
        {
            Ingredientes ing = lista[i];
            int cantidad = InventarioManager.Instancia.ObtenerCantidad(ing);
            slotsCreados[i].Configurar(ing, cantidad);
        }
    }
}
