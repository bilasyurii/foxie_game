using System.Collections;
using UnityEngine;

public class EnemyHealthAnimations : BaseHealthAnimations
{
    [SerializeField] private GameObject deathEffect = null;

    private SpriteRenderer spriteRenderer = null;

    private bool hurtFlickerStarted = false;
    private bool flickerShowing = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void PlayDeath()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

    public override void PlayHurt(int damage)
    {
        if (!hurtFlickerStarted)
        {
            hurtFlickerStarted = true;
            StartCoroutine("Flicker");
        }
        else
        {
            flickerShowing = true;
        }

        CreateFloatingText(damage.ToString(), damageColor);
    }

    private IEnumerator Flicker()
    {
        flickerShowing = true;
        while (flickerShowing)
        {
            flickerShowing = false;

            spriteRenderer.color = new Color(1f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        hurtFlickerStarted = false;
    }
}
