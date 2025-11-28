using DG.Tweening;
using UnityEngine;

public class SpinUpdate : MonoBehaviour
{
    public Transform cube;
    public float speed;

    private void Awake()
    {
        if(cube == null)
            cube = GetComponent<Transform>();
    }

    private void Update()
    {
        SpinCube();
    }

    private void SpinCube()
    {
        cube.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
