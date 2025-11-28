using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{
    public string playerTag = "Player";

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
        StartCoroutine(CoinSize());
        OnCollect();
    }


    protected virtual void OnCollect()
    {
        
    }

    public void OnDestroyItem()
    {
        Destroy(gameObject);
    }

    IEnumerator CoinSize()
    {
        gameObject.transform.DOScale(1.5f, .2f);
        yield return new WaitForSeconds(.32f);
        gameObject.transform.DOScale(0, .5f);
        yield return new WaitForSeconds(.27f);
        OnDestroyItem();
    }
}
