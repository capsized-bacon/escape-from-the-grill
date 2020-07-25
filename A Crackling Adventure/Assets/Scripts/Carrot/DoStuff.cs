using UnityEngine;

public class DoStuff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float jumpForce = 1f;
    
    private Rigidbody2D body;
    public LayerMask whatIsGround;
    public Transform root;

    public float speed;
    public bool moveRight;
    public bool isGrounded;
    
    public LayerMask enemyMask;
    Transform spriteTransform;
    private float width;

    void Start()
    {
        spriteTransform = this.transform;
        body = this.GetComponent<Rigidbody2D>();
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void FixedUpdate()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Move();
        Jump();
    }

    void Move()
    {
        Vector2 characterScale = transform.localScale;
        if (moveRight)
        {
            transform.Translate(1.5f * Time.deltaTime * speed, 0, 0);
            characterScale.x = 1;
        }
        else
        {
            transform.Translate(-1.5f * Time.deltaTime * speed, 0, 0);
            characterScale.x = -1;
        }
        transform.localScale = characterScale;
    }

    void Jump()
    {
        if (isGrounded)
        {
          //body.AddForce(new Vector2(10f, jumpForce));
            body.velocity = Vector2.up * jumpForce;
        }
    }

    private void IsGrounded()
    {
       // isGrounded = Physics2D.OverlapArea(root.position, root.position, whatIsGround);

        Vector2 lineCastPos = spriteTransform.position - spriteTransform.right * width;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.Log(isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("boundaries"))
        {
           Vector3 currRot = spriteTransform.eulerAngles;

            if(moveRight)
            {
                moveRight = false;
            } else
            {
                moveRight = true;
            }

            currRot.y += 180;
        }
    }
}
