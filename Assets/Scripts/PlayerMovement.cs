using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller = null;
    [SerializeField] private HealthBar healthBar = null;
    [SerializeField] private float runSpeed = 40f;

    private Animator animator;
    private Rigidbody2D rb;
    private Health health;
    private Weapon weapon;

    private float horizontalMove = 0f;
    private bool jumpRequested = false;
    private bool crouchRequested = false;
    private bool isFalling = false;
    private bool isHurt = false;
    private bool hurtEnded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        weapon = GetComponent<Weapon>();

        healthBar.SetCoefficient(1.0f, true);
    }

    private void Update()
    {
        HandleMovement();
        HandleAnimations();
    }

    private void HandleMovement()
    {
        horizontalMove = 0f;
        hurtEnded = false;

        if (isHurt)
        {
            if (!isFalling && Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                health.invincible = false;
                hurtEnded = true;
                weapon.canShoot = true;
            }
            else
            {
                return;
            }
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouchRequested = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchRequested = false;
        }
    }

    private void HandleAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (hurtEnded)
        {
            animator.SetBool("isHurt", false);
        }

        if (jumpRequested && !isFalling)
        {
            animator.SetBool("IsJumping", true);
        }
    }

    public void HandleHurt(bool directionToRight, float strengthX, float strengthY)
    {
        if (!isHurt)
        {
            healthBar.SetCoefficient(health.health / (float)health.maxHealth, true);
        }

        isHurt = true;
        health.invincible = true;

        weapon.canShoot = false;

        if (!directionToRight)
        {
            strengthX = -strengthX;
        }
        rb.AddForce(new Vector2(strengthX, rb.velocity.y + strengthY));
    }

    public void OnLanding()
    {
        isFalling = false;
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
    }

    public void OnFalling()
    {
        isFalling = true;
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouchRequested, jumpRequested);
        jumpRequested = false;
    }
}
