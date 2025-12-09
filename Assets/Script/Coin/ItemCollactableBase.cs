using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemCollactableBase : MonoBehaviour
{
    public string playerTag = "Player";
    public CircleCollider2D cicle;
    public ParticleSystem particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        Debug.Log("Collect");
        transform.DOKill();
        transform.localScale = Vector3.one;
        StartCoroutine(CoinSize());
        OnCollect();
    }


    protected virtual void OnCollect()
    {
        if(particleSystem != null)
        {
            cicle.enabled = false;
            particleSystem.Play();
        }
    }

    public void OnDestroyItem()
    {
        Destroy(gameObject);
    }

    IEnumerator CoinSize()
    {
        transform.DOKill();
        transform.localScale = Vector3.one;


        gameObject.transform.DOScale(1.5f, .2f);
        yield return new WaitForSeconds(.32f);
        gameObject.transform.DOScale(0, .5f);
        yield return new WaitForSeconds(5f);
        OnDestroyItem();
    }
}
