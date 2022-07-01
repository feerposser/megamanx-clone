using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : PlayerController
{

    private Animator anim;

    void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        base.Update();

        TurnSpriteDirection();

        Debug.Log("-----------------");
        Debug.Log((int)rb.velocity.x);
        Debug.Log(isGrounded);
        Debug.Log((int)rb.velocity.y);

        anim.SetInteger("xVelocity", (int)rb.velocity.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetInteger("yVelocity", (int)rb.velocity.y);
    }

    void TurnSpriteDirection()
    {
        if (sideState == SideState.RIGHT)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

}
