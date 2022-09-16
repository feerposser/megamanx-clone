using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;

    CircleCollider2D circleCollider;
    CapsuleCollider2D capsuleCollider;

    public bool isRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        isRight = false;

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        capsuleCollider.enabled = false;
    }

    void FixedUpdate()
    {
        ExecuteMovement();
    }

    void ExecuteMovement()
    {
        rb.velocity = !isRight ? Vector2.left * speed : Vector2.right * speed;
    }

    public void CircleToCapsuleCollider()
    {
        circleCollider.enabled = false;
        capsuleCollider.enabled = true;
    }

    private void ChangeCapsuleColliderYSize(float y)
    {
        if (capsuleCollider.enabled)
        {
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, y);
        }
    }

    public void FallingStage1()
    {
        ChangeCapsuleColliderYSize(0.24f);
    }

    public void FallingStage2()
    {
        ChangeCapsuleColliderYSize(0.18f);
    }

    public void FallingStage3()
    {
        ChangeCapsuleColliderYSize(0.167f);
    }
}
