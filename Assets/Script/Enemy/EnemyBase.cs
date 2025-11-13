using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 5;
    public LayerMask playerLayer;
    public Transform player;

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * 3.5f, Color.blue);

        if(CheckPlayer())
        {
            transform.position -= player.position * Time.deltaTime;
        }
    }

    private bool CheckPlayer()
    {
        return Physics2D.Raycast(transform.position, Vector2.right, 3.5f, playerLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.collider.GetComponent<HealthBase>();

        if(health != null)
        {
            health.Damage(damage);
        }
    }
}
