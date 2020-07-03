using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoStuff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float jumpForce = 1f;
    
    private Rigidbody2D body;

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
        //IsGrounded();


        //Vector2 characterScale = transform.localScale;

        if (moveRight)
        {
            transform.Translate(1.5f * Time.deltaTime * speed, 0, 0);
            //characterScale.x = 1;
        } else
        {
            transform.Translate(-1.5f * Time.deltaTime * speed, 0, 0);
            //characterScale.x = 1;
        }
        //transform.localScale = characterScale;

        if (isGrounded)
        {
            //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }

    }

    private void IsGrounded()
    {
        //isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
        Vector2 lineCastPos = spriteTransform.position - spriteTransform.right * width;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        isGrounded = !Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
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
            //spriteTransform.eulerAngles = currRot;
        }
    }
}
