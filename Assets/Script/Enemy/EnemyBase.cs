using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 5;

    [Header("References")]
    public LayerMask playerLayer;
    public Transform player;
    public Animator animator;
    public HealthBase healthBase;
    public Rigidbody2D rb;
    public float visionRage = 5f;
    public float speedMove = 10f;
    public BoxCollider2D enemyCollider;


    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (enemyCollider == null)
            enemyCollider = GetComponent<BoxCollider2D>();
    }

    private bool _isDead;

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * visionRage, Color.blue);

        if(CheckPlayer() && !_isDead)
        {
            rb.transform.position -= player.position * Time.deltaTime * speedMove;
        }
        OnEnemyKill();
    }

    private void OnEnemyKill()
    {
        if(healthBase._currentLife <= 0 && !_isDead)
        {
            _isDead = true;
            EnemyDeathAnimation();
            Debug.Log("Estou Morto");
            enemyCollider.enabled = false;
            rb.simulated = false;
        }
    }


    private bool CheckPlayer()
    {
        return Physics2D.Raycast(transform.position, Vector2.right, visionRage, playerLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.collider.GetComponent<HealthBase>();

        if(health != null && !_isDead)
        {
            health.Damage(damage);
            EnemyAttackAnimation();
        }
    }


    private void EnemyAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    private void EnemyDeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }

}
