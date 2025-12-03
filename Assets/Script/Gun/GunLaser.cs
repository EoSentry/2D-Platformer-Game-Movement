using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GunLaser : MonoBehaviour
{
    public MidasLaser midas;
    public Transform shootpoint;
    public Transform playerSideReference;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }
    }



    public void Shoot()
    {
        var laser = Instantiate(midas, shootpoint.position, Quaternion.identity);

        float dir = Mathf.Sign(playerSideReference.localScale.x);

        laser.side = dir;

        laser.transform.localScale = new Vector3(dir, laser.transform.localScale.y, 1);

        laser.transform.SetParent(shootpoint);
    }
}
