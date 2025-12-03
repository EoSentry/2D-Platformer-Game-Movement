using UnityEngine;

[CreateAssetMenu]
public class SOPlayer : ScriptableObject
{
    [Header("Player Setup")]
    public float moveSpeed = 10f;
    public float runSpeed = 20f;
    public float jumpForce = 15f;

    [Header("Player Animation")]
    public Animator animator;
    public float timeBetweenRotation = -1;
    public string boolRun = "run";
    public string triggerJump = "jump";
    public string boolDown = "down";
    public string triggerIdle = "idle";
}
