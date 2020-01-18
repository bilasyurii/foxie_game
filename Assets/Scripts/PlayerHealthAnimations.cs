using UnityEngine;

public class PlayerHealthAnimations : BaseHealthAnimations
{
    private Animator animator;

    protected static Color playerDamageColor;

    static PlayerHealthAnimations()
    {
        playerDamageColor = Color.red;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void PlayDeath()
    {
        //TODO
    }

    public override void PlayHurt(int damage)
    {
        animator.SetBool("isHurt", true);

        CreateFloatingText(damage.ToString(), playerDamageColor);
    }
}
