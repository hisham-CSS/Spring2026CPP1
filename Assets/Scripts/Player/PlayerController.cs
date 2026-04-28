using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask groundLayer;

    //Configurable variables
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    //1. Poll our input so that we can see what our input values are.
    //2. Move our player horizontally based on the horizontal input value.
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator anim;

    //state variables
    private bool _isGrounded;
    private GroundCheck groundCheck;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        groundCheck = new GroundCheck(col, rb, groundCheckRadius, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded by performing a circle overlap at the ground check position
        _isGrounded = groundCheck.CheckGrounded();

        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");

        if (horizontalInput != 0) SpriteFlip(horizontalInput);

        // Move the player horizontally based on the horizontal input value
        rb.linearVelocityX = horizontalInput * moveSpeed;

        if (jumpInput && _isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Update animator parameters
        anim.SetFloat("horizontalInput", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", _isGrounded);
        anim.SetFloat("yVel", rb.linearVelocityY);
    }

    void SpriteFlip(float horizontalInput) => sr.flipX = (horizontalInput < 0);      
    //if (sr.flipX && horizontalInput > 0 || !sr.flipX && horizontalInput < 0)
    //    sr.flipX = !sr.flipX;    
}
