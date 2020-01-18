using UnityEngine;
using UnityEngine.Events;

public class BaseMoveController : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCheck;
    [Range(0, .3f)] [SerializeField] protected float movementSmoothing = .05f;

    private const float groundedRadius = 0.2f;
    protected bool grounded;
    protected Rigidbody2D rb;
    protected bool facingRight = true;

    [Header("Base events")]
    [Space]

    public UnityEvent OnLandEvent;

    public UnityEvent OnFallEvent;
    protected bool falling = false;
    
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnFallEvent == null)
            OnFallEvent = new UnityEvent();
    }

    protected void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;

                if (!wasGrounded)
                {
                    falling = false;
                    OnLandEvent.Invoke();
                }

                break;
            }
        }

        if (!grounded)
        {
            if (wasGrounded || (!falling && rb.velocity.y < -0.01f))
            {
                OnFallEvent.Invoke();
                falling = true;
            }
        }
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
