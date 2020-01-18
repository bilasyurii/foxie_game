using UnityEngine;

public class FrogController : BaseMoveController
{
    [SerializeField] private float jumpHeight = 5;

    private float gravity;

    private new void Awake()
    {
        base.Awake();
        gravity = rb.gravityScale * Physics2D.gravity.y;
        facingRight = false;
    }

    public void Move(Vector3 target, bool jump)
    {
        if (jump && grounded)
        {
            grounded = false;

            float distanceX = target.x - rb.position.x;
            Vector2 jumpVelocity = CalculateJumpVelocity(distanceX, jumpHeight);

            rb.velocity = jumpVelocity;

            if (distanceX > 0 && !facingRight)
            {
                Flip();
            }
            else if (distanceX < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private Vector2 CalculateJumpVelocity(float distanceX, float jumpHeight)
    {
        float velocityY = Mathf.Sqrt(-2 * gravity * jumpHeight);
        float velocityX = distanceX / (2 * Mathf.Sqrt(-2 * jumpHeight / gravity));

        return new Vector2(velocityX, velocityY);
    }
}
