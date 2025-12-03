using DG.Tweening;
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


    private Vector2 _playerTransform;


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
        Debug.DrawRay(transform.position, _playerTransform * visionRage, Color.blue);
        Vector2 playerReference = (player.position - transform.position).normalized;
        _playerTransform = playerReference;

        if (CheckPlayer() && !_isDead)
        {
            Vector2 playerDirection = (player.position - transform.position).normalized;
            rb.transform.position += (Vector3)playerDirection * Time.deltaTime * speedMove;
        }
        OnEnemyKill();
        EnemyDirection();
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
        return Physics2D.Raycast(transform.position, _playerTransform, visionRage, playerLayer);
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

    public void EnemyDirection()
    {
        float dirX = player.position.x - transform.position.x;

        if(dirX > 0)
        {
            transform.DOScaleX(-1, .1f);
        }
        else if(dirX < 0)
        {
            transform.DOScaleX(1, .1f);
        }
    }

}
