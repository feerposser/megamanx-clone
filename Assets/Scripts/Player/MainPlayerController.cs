using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : PlayerController
{

    private Animator anim;

    private void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        base.Update();

        TurnSpriteDirection();

        anim.SetInteger("xVelocity", (int)rb.velocity.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetInteger("yVelocity", (int)rb.velocity.y);
        /*anim.SetBool("isWalljumping", isWallJumping);*/

        ComputeShoot();
    }

    private void TurnSpriteDirection()
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

    private void ComputeShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        FireSystem.instance.Shoot(0, sideState.ToString());
    }

}
