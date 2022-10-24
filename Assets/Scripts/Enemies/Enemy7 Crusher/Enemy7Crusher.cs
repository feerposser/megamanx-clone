using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Crusher : MonoBehaviour
{
    public enum CrushState { DEFAULT, PREPARETOCRUSH, CRUSING, RETURNING, RETURNED }

    Rigidbody2D rb;
    Enemy7 enemy7;

    [SerializeField] Transform enemy7Hook;
    [SerializeField] float speed;
    [SerializeField] CrushState crushState;

    private void Start()
    {
        crushState = CrushState.DEFAULT;
        rb = GetComponent<Rigidbody2D>();
        
        enemy7 = gameObject.transform.parent.GetComponent<Enemy7>();
    }

    private void FixedUpdate()
    {
        if (crushState.Equals(CrushState.CRUSING))
            rb.velocity = Vector2.down * speed;
        else if (crushState.Equals(CrushState.RETURNING))
            ReturnToOriginalPosition();
    }

    public void Crush()
    {
        crushState = CrushState.CRUSING;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Breakable Ground"))
            crushState = CrushState.RETURNING;
    }

    private void ReturnToOriginalPosition()
    {
        if (CrushIsInTheHook(0.38f))
        {
            rb.velocity = Vector2.zero;
            crushState = CrushState.RETURNED;
            enemy7.DidTheCrush();
        }
        else
        {
            rb.velocity = Vector2.up * speed;
        }
    }

    private bool CrushIsInTheHook(float distance)
    {
        return Vector2.Distance(transform.position, enemy7Hook.position) < distance;
    }
}
