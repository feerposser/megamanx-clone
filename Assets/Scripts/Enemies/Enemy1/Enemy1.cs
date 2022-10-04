using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;

    CircleCollider2D circleCollider;
    CapsuleCollider2D capsuleCollider;
    RaycastHit2D sideContact;

    public LayerMask sideContactLayer;
    public bool isRight;
    public float speed;

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
        WallContact();
    }

    void ExecuteMovement()
    {
        rb.velocity = !isRight ? Vector2.left * speed : Vector2.right * speed;
    }

    void WallContact()
    {
        if (isRight)
        {
            sideContact = Physics2D.Raycast(transform.position, Vector2.right, 1f, sideContactLayer);
            Debug.DrawRay(transform.position, Vector2.right, Color.red);
        } 
        else
        {
            sideContact = Physics2D.Raycast(transform.position, Vector2.left, 1f, sideContactLayer);
            Debug.DrawRay(transform.position, Vector2.left, Color.blue);
        }

        if (sideContact)
        {
            ChangeSideMovement();
        }
    }

    void ChangeSideMovement()
    {
        isRight = !isRight;
        transform.eulerAngles = isRight ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
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
