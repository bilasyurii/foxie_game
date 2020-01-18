using UnityEngine;

public class FightWall : MonoBehaviour
{
    public float showUpSpeed = 40f;

    bool isShowingUp = false;

    BoxCollider2D wallCollider;
    Transform graphicsTransform;

    private void Start()
    {
        wallCollider = GetComponent<BoxCollider2D>();
        graphicsTransform = transform.GetChild(0);
    }

    public void Activate()
    {
        wallCollider.enabled = true;
        isShowingUp = true;
    }

    public void FixedUpdate()
    {
        if (isShowingUp)
        {
            graphicsTransform.Translate(0, showUpSpeed * Time.deltaTime, 0);
            if(graphicsTransform.localPosition.y >= 0f)
            {
                isShowingUp = false;
                graphicsTransform.localPosition = Vector3.zero;
            }
        }
    }
}
