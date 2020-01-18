using UnityEngine;

public class FrogAI : MonoBehaviour
{
    [SerializeField] private FrogController controller = null;
    [SerializeField] private Transform target = null;
    [SerializeField] private GameObject dustPrefab = null;
    [SerializeField] private Transform dustSpawnpoint = null;
    [SerializeField] private int damage = 40;
    [SerializeField] private float knockX = 1000f;
    [SerializeField] private float knockY = 200f;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D[] targetColliders;
    private Collider2D ownCollider;

    private bool isFighting = false;
    private bool jumpRequested = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<Collider2D>();
        targetColliders = target.GetComponents<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!isFighting)
            return;

        controller.Move(target.position, jumpRequested);
        jumpRequested = false;
    }

    public void StartFight()
    {
        isFighting = true;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
        Instantiate(dustPrefab, dustSpawnpoint.position, Quaternion.identity);
        SetIgnoreTarget(false);
    }

    public void OnFalling()
    {
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsJumping", false);
        SetIgnoreTarget(true);
    }

    public void Jump()
    {
        if (isFighting)
        {
            jumpRequested = true;
            animator.SetBool("IsJumping", true);
        }
    }

    private void SetIgnoreTarget(bool ignore)
    {
        foreach (Collider2D collider in targetColliders)
        {
            Physics2D.IgnoreCollision(ownCollider, collider, ignore);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            Health playerHealth = collision.collider.GetComponent<Health>();
            playerHealth.TakeDamage(damage);

            PlayerMovement playerMovement = collision.collider.GetComponent<PlayerMovement>();
            playerMovement.HandleHurt(collision.transform.position.x > transform.position.x, knockX, knockY);
        }
    }
}
