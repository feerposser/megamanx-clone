using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : MonoBehaviour
{
    public enum SideState { LEFT, RIGHT }

    Rigidbody2D rb;
    [SerializeField] bool hasImpact;
    [SerializeField] float speed;
    [SerializeField] float destroyGameObjectTime;
    [SerializeField] SideState sideState = SideState.RIGHT;

    private Animator anim;

    
    void Start()
    {
        hasImpact = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5);
    }

    public void SetDirection(string direction)
    {
        direction = direction.ToUpper();

        sideState = direction.Equals("RIGHT") ? 
            SideState.RIGHT : SideState.LEFT;
    }

    private void FixedUpdate()
    {
        if (!hasImpact)
        {
            if (sideState.Equals(SideState.RIGHT))
            {
                rb.velocity = Vector2.right * speed;
            } else
            {
                rb.velocity = Vector2.left * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Reflect"))
        {
            hasImpact = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("contact");
            Destroy(gameObject, destroyGameObjectTime);
        }
        
    }
}
