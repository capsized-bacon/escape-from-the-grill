using System.Collections;
using System.Collections.Generic;
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
    // Variable for animation here:
    public Animator animator;

    // Health and living state check
    private int maxHealth;
    public int currentHealth;
    private bool isAlive;

    // Jesse: Adding Score variable.
    public int currentScore = 0;

    // Jesse: Adding Player instance.
    static Player instance; // [1]



    public static Player GetInstance()
    {
        return instance;
    }

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
    private float reserveJumpVelocity;
    public float jumpMultiplier;
    private float reserveSpeed;
    public float speedMultiplier;
    public int powerUpLength = 10;
    private bool isInvincible;
    private bool isSuperSpeed;
    private bool isSuperJump;
    private int speedCount = 0;
    private int jumpCount = 0;
    private int invincibilityCount = 0;

    private void Start()
    {
        // Jesse: Singleton pattern. He shall never die! https://wiki.unity3d.com/index.php/Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        // Initialising the boolean check for being alive, to be used for game over trigger.
        // isAlive = true;
        // Setting default health to 100, subject to change.
        maxHealth = 100;
        currentHealth = maxHealth;

        // Jesse: Set score to 0.
        currentScore = 0;

        // This completes the rigidbody shortcut by assigning rb to get the rigidbody component when the game starts.
        // As the variable is private, it knows that I'm looking for the rb of the actual object (ie the player or THIS).
        rb = GetComponent<Rigidbody2D>();

        // Setting jump parameters here during testing. Can take it off and alter the amount in Unity later.
        jumpVelocity = 21f;
        hangTime = .2f;
        jumpBufferLength = .01f;
        bounceOffVelocity = 8f;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Calling the methods that need to be checked per frame
        IsGrounded();
        FlipSprite();
        Move();
        Jump();
        IsAlive();
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    // This makes the sprite turn left and right when the player moves left and right. In hindsight, I should have
    // decided on the direction all sprites would face first. Probably other ways to do this code too. Doesn't matter, it works.
    private void FlipSprite()
    {
        Vector2 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
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
            animator.SetBool("IsJumping", true);
            rb.velocity = Vector2.up * jumpVelocity;
            jumpBufferCount = 0;
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        // If the space bar is let go early in the jump, the velocity of the jump will lessen to 
        // allow more control on jump height.
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    // Check if the player's feet are on the gronud. overlap area creates a rectangle from two points
    // and a third parameter to select what layer will trigger an overlap.
    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
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
        if (value < 0) // If negative value.
        {
            if (isInvincible == false)
            {
                FindObjectOfType<AudioManager>().Play("Hit");
                currentHealth += value;
                Debug.Log("Health: " + currentHealth);
            }
            else if (isInvincible == true)
            {
                Debug.Log("Health: " + currentHealth);
            }
        }
        else // If positive value.
        {
            currentHealth += value;
            Debug.Log("else Health: " + currentHealth);
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                Debug.Log("Health is at max! " + currentHealth);

            }
        }
    }



    // Jesse: Modify the player score variable based on value received from the caller.

    public void ModifyScore(int value)
    {
        currentScore += value;
        Debug.Log("Score: " + currentScore);
    }

    public void BounceBack(Collision2DSideType side)
    {
        if (side == Collision2DSideType.Top)

        {
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
        if (currentHealth <= 0)
        {
            isAlive = false;
            // Debug.Log("Game over!");
            GameOver();
        }

    }

    // Getters (Java style still works, however C# has additional usage when using it's own format).

    public int GetScore()
    {
        return currentScore;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void GameOver()
    {
        FindObjectOfType<GameManager>().GameOver();
    }

    //Detects when player collides with power up object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroys the power up object and initiates the super jump power up
        if (collision.tag == "SuperJump")
        {
            //Nested if prevents power ups stacking
            if (isSuperJump == false)
            {
                Destroy(collision.gameObject);
                reserveJumpVelocity = jumpVelocity;
                jumpMultiplier = 1.3f;
                jumpVelocity = jumpVelocity * jumpMultiplier;
                isSuperJump = true;
                playerColour();
                jumpCount = jumpCount + 1;
                StartCoroutine(ResetJump());
            }
            else if (isSuperJump == true)
            {
                Destroy(collision.gameObject);
                playerColour();
                jumpCount = jumpCount + 1;
                StartCoroutine(ResetJump());
            }

        }
        //Destroys the power up object and initiates the super speed power up
        if (collision.tag == "SuperSpeed")
        {
            //Nested if prevents power ups stacking
            if (isSuperSpeed == false)
            {
                Destroy(collision.gameObject);
                reserveSpeed = speed;
                speedMultiplier = 2f;
                speed = speed * speedMultiplier;
                isSuperSpeed = true;
                playerColour();
                speedCount = speedCount + 1;
                StartCoroutine(ResetSpeed());
            }
            else if (isSuperSpeed == true)
            {
                Destroy(collision.gameObject);
                playerColour();
                speedCount = speedCount + 1;
                StartCoroutine(ResetSpeed());
            }
        }
        //Destroys the power up object and initiates the invincibility power up
        if (collision.tag == "Invincibility")
        {
            Destroy(collision.gameObject);
            isInvincible = true;
            playerColour();
            invincibilityCount = invincibilityCount + 1;
            StartCoroutine(ResetInvincible());
        }
    }

    //Creates a time for the powerup and resets the player back to normal jump velocity after the timer expires
    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(powerUpLength);
        jumpCount = jumpCount - 1;
        //The while loop enables the player to grab multiple of the same power up and have their timers stack
        while (speedCount > 0)
        {
            yield return new WaitForSeconds(powerUpLength);
            jumpCount = jumpCount - 1;
        }
        jumpVelocity = reserveJumpVelocity;
        isSuperJump = false;
        playerColour();
    }
    //Creates a time for the powerup and resets the player back to normal speed after the timer expires
    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(powerUpLength);
        speedCount = speedCount - 1;
        //The while loop enables the player to grab multiple of the same power up and have their timers stack
        while (speedCount > 0)
        {
            yield return new WaitForSeconds(powerUpLength);
            speedCount = speedCount - 1;
        }
        speed = reserveSpeed;
        isSuperSpeed = false;
        playerColour();
    }

    //Creates a time for the powerup and resets the player back to normal after the timer expires
    private IEnumerator ResetInvincible()
    {
        yield return new WaitForSeconds(powerUpLength);
        //The while loop enables the player to grab multiple of the same power up and have their timers stack
        invincibilityCount = invincibilityCount - 1;
        while (invincibilityCount > 0)
        {
            yield return new WaitForSeconds(powerUpLength);
            invincibilityCount = invincibilityCount - 1;
        }
        isInvincible = false;
        playerColour();
    }

    //Changes the players colour depending on what combination of pwer ups are active
    private void playerColour()
    {
        if (isSuperJump == true && isSuperSpeed == false && isInvincible == false)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (isSuperJump == false && isSuperSpeed == true && isInvincible == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (isSuperJump == false && isSuperSpeed == false && isInvincible == true)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (isSuperJump == true && isSuperSpeed == true && isInvincible == false)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.65f, 0f, 1f);//Purple
        }
        else if (isSuperJump == true && isSuperSpeed == false && isInvincible == true)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (isSuperJump == false && isSuperSpeed == true && isInvincible == true)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.55f, 0f);//Orange
        }
        else if (isSuperJump == true && isSuperSpeed == true && isInvincible == true)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if (isSuperJump == false && isSuperSpeed == false && isInvincible == false)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
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



// REFERENCES



/*[1] “Unity Tutorial: Preserving Data between Scene Loading/Switching...”. [Online].
Available: https://www.youtube.com/watch?v=WchH-JCwVI8.
[Accessed: 30-Jun.-2020].*/
