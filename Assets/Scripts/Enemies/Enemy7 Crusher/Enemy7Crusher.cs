using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Crusher : MonoBehaviour
{
    public enum CrushState { DEFAULT, PREPARETOCRUSH, CRUSING, PREPARETORETURN, RETURNING }

    Rigidbody2D rb;
    Transform originalPosition;

    [SerializeField] float speed;
    [SerializeField] CrushState crushState;

    private void Start()
    {
        crushState = CrushState.DEFAULT;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (crushState.Equals(CrushState.CRUSING))
        {
            rb.velocity = Vector2.down * speed;
            Debug.Log("crushing");
        }
    }

    public void Crush()
    {
        crushState = CrushState.CRUSING;
    }

    private void ReturnToOriginalPosition()
    {

    }
}
