using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform shootPoint;
    public float timeBetweenToShoot = .2f;
    public Transform playerSideReference;

    private Coroutine _currentCoroutine;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
        }

    }

    IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenToShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = shootPoint.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }


}
