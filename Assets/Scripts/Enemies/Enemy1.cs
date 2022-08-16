using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    Rigidbody2D rb;
    
    [SerializeField] float speed;

    public bool isRight;

    void Start()
    {
        health = 100;
        isRight = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ExecuteMovement();
        PushDown();
    }

    void PushDown()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + -2);
    }

    void ExecuteMovement()
    {
        if (!isRight)
        {
            rb.velocity = Vector2.left * speed;
        }
        else
        {
            rb.velocity = Vector2.right * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            Debug.Log("set damage to player");
        }
    }
}
