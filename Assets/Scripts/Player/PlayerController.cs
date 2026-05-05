using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    #region Settings and Configurable Variables
    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask groundLayer;

    //Configurable variables
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxLives = 9;

    [Header("Powerup Settings")]
    [SerializeField] private float jumpForcePowerup = 15f;
    [SerializeField] private float initalPowerupDuration = 5f;


    #endregion

    #region State Variables
    //C++ style getters and setters - not recommended in C#, but here for demonstration purposes
    //public void SetLives(int value)
    //{
    //    if (value > maxLives)
    //    {
    //        _lives = maxLives;
    //    }
    //    else if (value < 0)
    //    {
    //        _lives = 0;
    //        //go to game over
    //    }
    //    else
    //    {
    //        _lives = value;
    //    }
    //}
    //public int GetLives()
    //{
    //    return _lives;
    //}

    //if lives is greater than maxLives, set it to maxLives
    //if lives is less than 0, set it to 0 - and go to game over

    private int _lives = 3;
    public int lives
    {
        get {  return _lives; }
        set
        {
            if (value > maxLives)
            {
                _lives = maxLives;
            }
            else if (value < 0)
            {
                _lives = 0;
                //go to game over
            }
            else
            {
                _lives = value;
            }

            Debug.Log($"Lives have changed to {_lives}");
        }
    }

    private int _score = 0;
    public int score
    {
        get { return _score; }
        set 
        {    
            value = Mathf.Max(0, value); 
            _score = value; 
        }
    }

    private bool _isGrounded;
    private GroundCheck groundCheck;

    private float currentPowerupDuration = 0f;
    private float initialJumpForce = 5f;

    private Coroutine jumpForceCoroutine = null;
    #endregion

    //1. Poll our input so that we can see what our input values are.
    //2. Move our player horizontally based on the horizontal input value.

    #region Component References
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator anim;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        groundCheck = new GroundCheck(col, rb, groundCheckRadius, groundLayer);

        initialJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        // Check if the player is grounded by performing a circle overlap at the ground check position
        _isGrounded = groundCheck.CheckGrounded();

        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool fireInput = Input.GetButtonDown("Fire1");

        if (horizontalInput != 0) SpriteFlip(horizontalInput);

        // Move the player horizontally based on the horizontal input value
        rb.linearVelocityX = horizontalInput * moveSpeed;

        if (jumpInput && _isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (fireInput && clipInfo[0].clip.name != "Fire")
        {
            anim.SetTrigger("Fire");
        }

        if (clipInfo[0].clip.name == "Fire")
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Update animator parameters
        anim.SetFloat("horizontalInput", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", _isGrounded);
        anim.SetFloat("yVel", rb.linearVelocityY);
    }

    void SpriteFlip(float horizontalInput) => sr.flipX = (horizontalInput < 0);
    //if (sr.flipX && horizontalInput > 0 || !sr.flipX && horizontalInput < 0)
    //    sr.flipX = !sr.flipX;


    public void JumpForceChange()
    {
        if (jumpForceCoroutine != null)
        {
            StopCoroutine(jumpForceCoroutine);
            jumpForceCoroutine = null;
            jumpForce = initialJumpForce;
        }

        jumpForceCoroutine = StartCoroutine(JumpForceChangeCoroutine());
    }

    IEnumerator JumpForceChangeCoroutine()
    {
        currentPowerupDuration = initalPowerupDuration + currentPowerupDuration;
        jumpForce = jumpForcePowerup;

        while (currentPowerupDuration > 0)
        {
            currentPowerupDuration -= Time.deltaTime;
            Debug.Log($"Jump Powerup Time Remaining {currentPowerupDuration}");
            yield return null;
        }

        jumpForce = initialJumpForce;
        jumpForceCoroutine = null;
        currentPowerupDuration = 0f;
    }

    //oncollisionenter, oncollisionexit, oncollisionstay, ontriggerenter, ontriggerexit, and ontriggerstay functions for the player to interact with the environment and other objects, such as picking up items or taking damage from enemies
    //the collision functions are specifically for blocking collisions - this means that both colliders are solid and at least one of the colliding bodies is a dynamic rigidbody.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("PlayerController: OnTriggerEnter2D called with collider " + collision.name);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("PlayerController: OnTriggerExit2D called with collider " + collision.name);
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("PlayerController: OnTriggerStay2D called with collider " + collision.name);
    //}
}
