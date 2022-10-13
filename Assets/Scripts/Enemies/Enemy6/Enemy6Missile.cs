using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Missile : MonoBehaviour
{
    Rigidbody2D rb;
    float trailTimer;

    [SerializeField] float speed;
    [SerializeField] GameObject trail;
    [SerializeField] GameObject deployExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        if (trailTimer < Time.time)
        {
            CreateTrail();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.left * speed;
    }

    private void CreateTrail()
    {
        trailTimer = Time.time + 0.1f;
        GameObject t = Instantiate(trail, transform.position + new Vector3(.3f, 0, 0), Quaternion.identity);
        Destroy(t, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            Instantiate(deployExplosion, transform.position, Quaternion.identity).GetComponent<DeployExplosion>().Explode();
            Destroy(gameObject);
        }
    }
}
