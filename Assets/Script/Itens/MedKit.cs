using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public int recoverer = 5;
    public BoxCollider2D box;

    [Header("SFX")]
    public AudioSource kitSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBase health = collision.GetComponent<HealthBase>();

            if (health != null)
            {
                RecoverHealth(health);
                StartCoroutine(MedKitSize());
            }
        }
    }

    public void RecoverHealth(HealthBase health)
    {
        if(health._currentLife < health.Life && health.isPlayer)
        {
            health._currentLife += recoverer;
            if(health._currentLife > health.Life)
                health._currentLife = health.Life;
            health.lifeBar.fillAmount = (float)health._currentLife / health.Life;
        }
    }

    IEnumerator MedKitSize()
    {
        kitSFX.Play();

        transform.DOScale(1.2f, .2f).SetEase(Ease.OutBack);
        box.enabled = false;
        yield return new WaitForSeconds(.30f);
        transform.DOScale(0, .2f);
        yield return new WaitForSeconds(.35f);
        Destroy(gameObject);
    }
}
