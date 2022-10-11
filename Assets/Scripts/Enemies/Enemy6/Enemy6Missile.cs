using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Missile : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    void Update()
    {
        rb.velocity = Vector2.left * speed;
    }
}
