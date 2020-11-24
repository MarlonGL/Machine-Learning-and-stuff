using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformBehaviour : MonoBehaviour
{
    public BoxCollider2D upperCol;
    public float waitTime, jumpForce, offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Agent")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            if (collision.relativeVelocity.y <= 0f)
            {
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }
            }
        }
    }
}
