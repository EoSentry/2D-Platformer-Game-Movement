using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int Life = 10;
    public bool destroyOnKill;
    public Transform prefab;
    public bool isPlayer = false;
    public Ease ease = Ease.OutBack;

    public int _currentLife;

    private void Awake()
    {
        _currentLife = Life;
    }

    public void Damage(int damage)
    {
        Debug.Log("Tomei dano");
        _currentLife -= damage;
        Kill();
    }

    private void Kill()
    {
        if(_currentLife <= 0 && isPlayer)
        {
            Debug.Log("Vida 0");
            StartCoroutine(CharacterAnimatonDeath());
        }

        else if(_currentLife <= 0)
        {
            StartCoroutine(CharacterAnimatonDeath());
        }
    }

    IEnumerator CharacterAnimatonDeath()
    {
        if (destroyOnKill)
        {
            transform.DOScale(1.5f, .3f).SetEase(ease);
            yield return new WaitForSeconds(.1f);
            transform.DOScale(0, .3f);
        }

        yield return new WaitForSeconds(.35f);
        if(isPlayer && destroyOnKill)
        {
            Destroy(gameObject);
        }
    }
}
