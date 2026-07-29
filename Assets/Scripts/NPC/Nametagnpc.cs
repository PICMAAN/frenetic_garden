using UnityEngine;
using TMPro;

// Etiqueta de nombre que siempre esta visible arriba de la cabeza del NPC
// Solo aparece cuando tiene un platillo pendiente
public class Nametagnpc : MonoBehaviour
{
    public TMP_Text textoNombre;

    public void MostrarNombre(string nombre)
    {
        if (textoNombre != null)
        {
            textoNombre.text = nombre;
        }
    }
}