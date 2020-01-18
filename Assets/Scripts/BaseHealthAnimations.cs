using UnityEngine;

abstract public class BaseHealthAnimations : MonoBehaviour, IHealthAnimationManager
{
    [SerializeField] private GameObject floatingTextPrefab = null;

    protected static Color damageColor, healingColor;

    [SerializeField] protected Transform floatingTextPosition = null;

    static BaseHealthAnimations()
    {
        damageColor = Color.yellow;
        healingColor = Color.green;
    }

    public abstract void PlayDeath();
    public abstract void PlayHurt(int damage);

    public void CreateFloatingText(string text, Color color)
    {
        GameObject floatingText = Instantiate(floatingTextPrefab,
                                              floatingTextPosition.position, 
                                              Quaternion.identity);

        TextMesh textMesh = floatingText.transform.GetChild(0).GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
    }
}
