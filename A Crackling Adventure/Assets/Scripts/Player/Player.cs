using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask whatIsGround;
    public Transform groundCheck1, groundCheck2;

    private bool isGrounded;
    public float speed;
    private float moveInput;
    public float jumpVelocity;
    public float hangTime;
    private float hangCounter;
    public float jumpBufferLength;
    private float jumpBufferCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Setting jump velocity here during testing. Can take it off and alter the amount in Unity later.
        jumpVelocity = 20f;
        hangTime = .2f;
        jumpBufferLength = .01f;
    }

    private void Update()
    {
        IsGrounded();
        FlipSprite();
        Move();
        Jump();
    }

    private void FlipSprite()
    {
        Vector2 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = 1;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = -1;
        }
        transform.localScale = characterScale;
    }

    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Jump()
    {

        CoyoteTime();
        JumpBuffer();

        if (jumpBufferCount >= 0 && hangCounter > 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            jumpBufferCount = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
    }

    private void CoyoteTime()
    {
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
    }

    private void JumpBuffer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }
    }
}