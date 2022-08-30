using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeSystem;

public class MainPlayerController : PlayerController
{

    private Animator anim;
    OnDamageSystem onDamage;

    public bool freeze;
    [SerializeField] private float freezeTime;

    private void Start()
    {
        base.Start();

        onDamage = GetComponent<OnDamageSystem>();
        onDamage.onDamage += OnDamage;

        anim = GetComponent<Animator>();
        freeze = false;
    }

    private void Update()
    {
        if (!freeze)
        {
            base.Update();

            TurnSpriteDirection();

            anim.SetInteger("xVelocity", (int)rb.velocity.x);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetInteger("yVelocity", (int)rb.velocity.y);
            /*anim.SetBool("isWalljumping", isWallJumping);*/

            ComputeShoot();
        }
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

    private void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
    {
        StartCoroutine("Freeze");
    }

    IEnumerator Freeze()
    {
        freeze = true;
        yield return new WaitForSeconds(freezeTime);
        freeze = false;
    }

}
