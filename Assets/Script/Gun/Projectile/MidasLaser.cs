using UnityEngine;

public class MidasLaser : MonoBehaviour
{
    public Color midasColor = new Color(1f, 0.85f, 0.3f);
    public float side = 1;


    private void Awake()
    {
        Destroy(gameObject, .38f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyBase>();

        if(enemy)
        {
            MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();
            
            foreach(var s in scripts)
            {
                s.enabled = false;
            }

            var rb = enemy.GetComponent<Rigidbody2D>();
            if(rb != null)
                rb.simulated = false;

            var box = enemy.GetComponent<Collider2D>();
            if(rb != null)
                box.enabled = false;

            var anim = enemy.GetComponentsInChildren<Animator>();
            foreach(var an in anim)
                an.enabled = false;

            var sprite = enemy.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sr in sprite) { sr.color = midasColor; }
                
        }
    }


}
