using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private SpriteRenderer barRenderer;

    private const float speed = 1f;

    private bool animating = false, automaticColor;
    private float current = 0, target;

    private void Awake()
    {
        bar = transform.Find("Bar");
        barRenderer = bar.Find("BarSprite").GetComponent<SpriteRenderer>();
    }

    public void SetCoefficient(float coef, bool autoColor = false, bool animate = true)
    {
        if (animate)
        {
            automaticColor = autoColor;

            if (!animating)
            {
                target = coef;
                animating = true;
            }
            else
            {
                current = target;
                target = coef;
            }
        }
        else
        {
            ChangeCoefficient(coef, autoColor);
        }
    }

    private void ChangeCoefficient(float coef, bool autoColor)
    {
        bar.localScale = new Vector3(coef, 1f);

        if (autoColor)
        {
            if (coef <= 0.25f)
            {
                barRenderer.color = new Color(1f, 0.3f, 0.15f);
            }
            else if (coef <= 0.4f)
            {
                barRenderer.color = new Color(1f, 1f, 0.15f);
            }
            else
            {
                barRenderer.color = new Color(0.15f, 1f, 0.15f);
            }
        }
    }

    public void SetColor(Color color)
    {
        barRenderer.color = color;
    }

    private void FixedUpdate()
    {
        float sign, diff, step;

        if (animating)
        {
            diff = target - current;
            sign = Mathf.Sign(diff);
            step = sign * speed * Time.deltaTime;

            if (Mathf.Abs(diff) <= step)
            {
                current = target;
                animating = false;
            }
            else
            {
                current += step;
            }

            ChangeCoefficient(current, automaticColor);
        }
    }
}
