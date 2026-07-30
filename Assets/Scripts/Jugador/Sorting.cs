using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortingDinamico : MonoBehaviour
{
    // Una base alta para asegurar que siempre esté por encima del suelo.
    [SerializeField] private int baseSortingOrder = 5000;
    // Multiplicador para la precisión de la posición Y.
    [SerializeField] private int precisión = 100;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        // El cálculo ahora es: Base Alta + (-PosiciónY * Precisión)
        // Ejemplo con Y=4.6: 5000 + (-4.6 * 100) = 5000 - 460 = 4540 (Order in Layer positivo y alto)
        int newOrder = baseSortingOrder + (int)(-transform.position.y * precisión);
        spriteRenderer.sortingOrder = newOrder;
    }
}