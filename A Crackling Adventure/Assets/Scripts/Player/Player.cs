using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Rigidbody shortcut: typing rb is easier than the whole thing.
    private Rigidbody2D rb;
    // Opening up the option to choose what layer is associated to checking player is grounded.
    public LayerMask whatIsGround;
    // These points are chosen in Unity and called when checking player is grounded.
    public Transform groundCheck1, groundCheck2;
    
    // Health and living state check
    public int health;
    private int maxHealth;
    private bool isAlive;

    // Variables related to movement
    private bool isGrounded;
    public float speed;
    private float moveInput;
    public float jumpVelocity;
    public float hangTime;
    private float hangCounter;
    public float jumpBufferLength;
    private float jumpBufferCount;
    public float bounceOffVelocity;

    private void Start()
    {
        // Initialising the boolean check for being alive, to be used for game over trigger.
        isAlive = true;
        // Setting default health to 100, subject to change.
        maxHealth = 100;
        health = maxHealth;

        // This completes the rigidbody shortcut by assigning rb to get the rigidbody component when the game starts.
        // As the variable is private, it knows that I'm looking for the rb of the actual object (ie the player or THIS).
        rb = GetComponent<Rigidbody2D>();

        // Setting jump parameters here during testing. Can take it off and alter the amount in Unity later.
        jumpVelocity = 20f;
        hangTime = .2f;
        jumpBufferLength = .01f;
        bounceOffVelocity = 8f;
    }

    private void Update()
    {
        // Calling the methods that need to be checked per frame
        IsGrounded();
        FlipSprite();
        Move();
        Jump();
        //IsAlive();
    }

    // This makes the sprite turn left and right when the player moves left and right. In hindsight, I should have
    // decided on the direction all sprites would face first. Probably other ways to do this code too. Doesn't matter, it works.
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

    // Simple movement left and right, using getaxis allows the controller to read any relevant value (arrow keys, wasd, and probably controller?).
    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

  
    private void Jump()
    {
        // CoyoteTime = the hang time in the air to let player adjust themselves
        CoyoteTime();
        // JumpBuffer = a way to let the player's jump input still read a few moments before they hit the ground. 
        // It's a polish thing.
        JumpBuffer();

        // if the jump key is pressed within x amount of time of hitting the ground, still jump, 
        // and reset the counters used to calculate that time.

        if (jumpBufferCount >= 0 && hangCounter > 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            jumpBufferCount = 0;
        }

        // If the space bar is let go early in the jump, the velocity of the jump will lessen to 
        // allow more control on jump height.
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    // Check if the player's feet are on the gronud. overlap area creates a rectangle from two points
    // and a third parameter to select what layer will trigger an overlap.
    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
    }

    private void CoyoteTime()
    {
        // if player is in the air, the hangtime counter will decrease. The counter determines how much time
        // you spend in "coyote time". The counter resets when you hit the ground.
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
        // this resets the jump buffer counter at the start of every new jump. Note deltaTime is the time in seconds
        // for the last frame to complete, which makes time run steady regardless of framerate.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }
    }

    // Modify the player health variable based on value received from caller.
    public void ModifyHealth(int value)
    {
        health += value;
        Debug.Log("Health: " + health);
        GameObject.Find("TestText").GetComponent<Text>().text = "Health: " + health.ToString();
    }

    public void BounceBack(Collision2DSideType side)
    {
        if(side == Collision2DSideType.Top) {
            rb.velocity = Vector2.up * bounceOffVelocity;
        }
/*        else if(side == Collision2DSideType.Left)
        {
            rb.velocity = Vector2.left * bounceOffVelocity;
            Debug.Log("Bounce Left");
        }
        else if (side == Collision2DSideType.Right)
        {
            rb.velocity = Vector2.right * bounceOffVelocity;
            Debug.Log("Bounce Right");
        }
        else
        {
            Debug.Log("Whoa, how did it colide there?");
        }*/
    }

    // check if player is dead. This needs to link to game over script.
    private void IsAlive()
    {
        if (health <= 0)
        {
            isAlive = false;
        } 
      
    }

    // This was a previous attempt at grounding using boxcast. I didn't stick with it because I couldn't get it
    // working effectively, and my overlap method felt more robust for handling odd terrain like diagonals.
    // 
    //private bool isGrounded()
    //{
    //    RaycastHit2D box = Physics2D.BoxCast(groundCheck.bounds.center, groundCheck.bounds.size, 0f, Vector2.down, platformLayer, 0, 5f);
    //    Debug.Log(box.collider);
    //    return box.collider != null;
    //}
}