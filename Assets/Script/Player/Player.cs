using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public SOPlayer soPlayer;


    [Header("Player Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public LayerMask groundedLayer;

    [Header("Player Inputs")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode jumpInput = KeyCode.Space;
    public KeyCode runInput = KeyCode.LeftControl;

    [Header("Player Animation")]
    public Animator _currentPlayer;

    [Header("Player Particles")]
    public ParticleSystem walk;
    public ParticleSystem jump;

    public Rigidbody2D rb { get; private set; }
    private float _currentSpeed;
    private bool _isMovement;

    

    #region Unity Functions

    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        _currentPlayer = Instantiate(soPlayer.animator, transform);
    }

    private void Update()
    {
        //PlayerMove();
        Debug.DrawRay(transform.position, Vector2.down * .6f, Color.red);
        PlayParticles();
    }

    private void FixedUpdate()
    {
        //PlayerJump();
    }

    #endregion

    #region Player Functions

    #region Deprecated PlayerMove Code
    public void PlayerMove()
    {
        if (Input.GetKey(runInput))
            _currentSpeed = soPlayer.runSpeed;
        else
            _currentSpeed = soPlayer.moveSpeed;

        if(Input.GetKey(right))
        {
            rb.linearVelocity = new Vector2(_currentSpeed, rb.linearVelocity.y);
            soPlayer.animator.SetBool(soPlayer.boolRun, true);
            if (rb.transform.localRotation.x != 1)
            {
                rb.transform.DOScaleX(1, soPlayer.timeBetweenRotation);
            }
        }

        else if(Input.GetKey(left))
        {
            rb.linearVelocity = new Vector2(-_currentSpeed, rb.linearVelocity.y);
            soPlayer.animator.SetBool(soPlayer.boolRun, true);
            if(rb.transform.localRotation.x != -1)
            {
                rb.transform.DOScaleX(-1, soPlayer.timeBetweenRotation);
            }
        }

        else
        {
            soPlayer.animator.SetBool(soPlayer.boolRun, false);
        }
        
        if(rb.linearVelocity.x > 0)
        {
            rb.linearVelocity += friction;
        }

        else if(rb.linearVelocity.x < 0)
        {
            rb.linearVelocity -= friction;
        }
        
    }

    public void PlayerJump()
    {
        if(Input.GetKey(jumpInput) && PlayerGroundedCheck())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, soPlayer.jumpForce);
            soPlayer.animator.SetTrigger("jump");
        }
    }
    #endregion

    public bool PlayerGroundedCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, .6f, groundedLayer);
    }

    private void PlayParticles()
    {
        if(PlayerGroundedCheck())
        {
            walk.Play();
            Debug.Log("Particulas de andar");
        }

        if(!PlayerGroundedCheck())
        {
            walk.Stop();
            Debug.Log("Particulas de andar desativadas");
        }

        if (Input.GetKeyDown(KeyCode.Space) && PlayerGroundedCheck())
        {
            jump.Play();
        }

        else
            jump.Stop();

    }

    #endregion
}
