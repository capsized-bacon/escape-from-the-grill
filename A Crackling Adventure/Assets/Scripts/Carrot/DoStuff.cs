using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoStuff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float jumpForce = 1f;
    
    Rigidbody2D body;

    public float Speed;
    public bool MoveRight;
    public bool Jump;
    public bool Grounded;
    
    public LayerMask enemyMask;
    Transform spriteTransform;
    float width;

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
        Vector2 lineCastPos = spriteTransform.position - spriteTransform.right * width;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        Grounded = !Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.Log(Grounded);
/*        if (Grounded)
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }*/

        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * Speed, 0, 0);
        } else
        {
            transform.Translate(-2 * Time.deltaTime * Speed, 0, 0);
        }

        if(Grounded)
        {
            //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("boundaries"))
        {
           Vector3 currRot = spriteTransform.eulerAngles;

            if(MoveRight)
            {
                MoveRight = false;
            } else
            {
                MoveRight = true;
            }

            currRot.y += 180;
            //spriteTransform.eulerAngles = currRot;
        }
    }
}
