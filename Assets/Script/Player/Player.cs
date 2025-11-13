using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Setup")]
    public float moveSpeed = 10f;
    public float runSpeed = 20f;
    public float jumpForce = 15f;
    public Vector2 friction = new Vector2(.1f, 0);
    public LayerMask groundedLayer;

    [Header("Player Inputs")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode jump = KeyCode.Space;
    public KeyCode runInput = KeyCode.LeftControl;

    private Rigidbody2D rb;
    private float _currentSpeed;
    private bool _isMovement;

    #region Unity Functions

    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMove();
        Debug.DrawRay(transform.position, Vector2.down * .6f, Color.red);
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    #endregion

    #region Player Functions

    public void PlayerMove()
    {
        if (Input.GetKey(runInput))
            _currentSpeed = runSpeed;
        else
            _currentSpeed = moveSpeed;

        if(Input.GetKey(right))
        {
            rb.linearVelocity = new Vector2(_currentSpeed, rb.linearVelocity.y);
        }

        if(Input.GetKey(left))
        {
            rb.linearVelocity = new Vector2(-_currentSpeed, rb.linearVelocity.y);
        }
        
        if(rb.linearVelocity.x > 0)
        {
            //rb.linearVelocity -= friction;
        }

        else if(rb.linearVelocity.x < 0)
        {
            //rb.linearVelocity += friction;
        }
        
    }

    public void PlayerJump()
    {
        if(Input.GetKey(jump) && PlayerGroundedCheck())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public bool PlayerGroundedCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, .6f, groundedLayer);
    }

    #endregion
}
