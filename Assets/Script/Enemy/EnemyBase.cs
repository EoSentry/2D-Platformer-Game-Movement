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

    private bool _isDead;

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * visionRage, Color.blue);

        if(CheckPlayer())
        {
            rb.transform.position -= player.position * Time.deltaTime;
        }
        OnEnemyKill();
    }

    private void OnEnemyKill()
    {
        if(healthBase._currentLife <= 0 && !_isDead)
        {
            _isDead = true;
            PlayerDeathAnimation();
            Debug.Log("Estou Morto");
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
            PlayerAttackAnimation();
        }
    }


    private void PlayerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    private void PlayerDeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }

}
