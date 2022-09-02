using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    public bool isRight;

    void Start()
    {
        isRight = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ExecuteMovement();
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
