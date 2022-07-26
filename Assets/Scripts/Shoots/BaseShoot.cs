using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : MonoBehaviour
{

    private Animator anim;
    protected Rigidbody2D rb;

    public enum SideState { LEFT, RIGHT }

    public float speed;
    public float destroyGameObjectTime;
    public SideState sideState = SideState.RIGHT;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5);
    }

    public void SetDirection(string direction)
    {
        direction = direction.ToUpper();

        sideState = direction.Equals("RIGHT") ? SideState.RIGHT : SideState.LEFT;
    }

    private void FixedUpdate()
    {
        if (sideState.Equals(SideState.RIGHT))
        {
            rb.velocity = Vector2.right * speed;
        } else
        {
            rb.velocity = Vector2.left * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            anim.SetTrigger("contact");
            Destroy(gameObject, destroyGameObjectTime);
        }
    }
}
