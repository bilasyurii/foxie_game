using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : BaseMoveController
{
    [SerializeField] private float jumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;
    [SerializeField] private bool airControl = false;
    [SerializeField] private Transform ceilingCheck = null;
    [SerializeField] private Collider2D crouchDisableCollider = null;

    const float ceilingRadius = .2f;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Header("Character events")]
    [Space]

    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    private Vector3 currentVelocity = Vector3.zero;

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        if (grounded || airControl)
        {

            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= crouchSpeed;

                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = false;
            }
            else
            {
                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = true;

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
        if (grounded && jump)
        {
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

}