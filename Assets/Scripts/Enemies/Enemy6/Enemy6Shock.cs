using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Shock : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    void Update()
    {
        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
    }
}
