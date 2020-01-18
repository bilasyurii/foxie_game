using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Renderer textRenderer;

    private void Awake()
    {
        textRenderer = GetComponentInChildren<Renderer>();
        textRenderer.sortingLayerName = "Foreground";
        textRenderer.sortingOrder = 500;

        Destroy(gameObject, 1f);
    }
}
