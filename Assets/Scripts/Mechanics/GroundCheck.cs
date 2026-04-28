using UnityEngine;

public class GroundCheck
{
    private bool isGrounded = false;

    private LayerMask groundLayer;
    private Rigidbody2D rb;
    private Collider2D col;
    private float radius;

    private Vector2 groundCheckPos => new Vector2(col.bounds.center.x, col.bounds.min.y);
    public GroundCheck(Collider2D col, Rigidbody2D rb, float radius, LayerMask groundLayer)
    {
        this.col = col;
        this.rb = rb;
        this.radius = radius;
        this.groundLayer = groundLayer;
    }

    public bool CheckGrounded()
    {
        if (!isGrounded && rb.linearVelocityY <= 0 || isGrounded)
            isGrounded = Physics2D.OverlapCircle(groundCheckPos, radius, groundLayer);

        return isGrounded;
    }
}
