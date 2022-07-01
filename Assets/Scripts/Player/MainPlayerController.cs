using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : PlayerController
{

    private Animator anim;

    void Start()
    {
        base.Start();

        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        base.Update();

        TurnSpriteDirection();

        /*anim.SetInteger("xVelocity", (int)rb.velocity.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetInteger("yVelocity", (int)rb.velocity.y);
        anim.SetBool("isWalljumping", isWallJumping);*/
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
