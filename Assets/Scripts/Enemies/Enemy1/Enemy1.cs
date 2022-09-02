using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
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
        rb.velocity = !isRight ? Vector2.left * speed : Vector2.right * speed;
    }
}
