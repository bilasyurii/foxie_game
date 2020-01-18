using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 40;

    [HideInInspector] public bool isEnemy;
    private Rigidbody2D rb = null;
    private GameHandler gameHandler = null;

    private void Awake()
    {
        gameHandler = GameHandler.instance;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (rb.transform.position.x <= gameHandler.LeftBound.position.x ||
            rb.transform.position.x >= gameHandler.RightBound.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.name == "Player") == isEnemy)
        {
            Health targetHealth = collision.collider.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
