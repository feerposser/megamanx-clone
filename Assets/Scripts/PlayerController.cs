using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public enum JumpState { grounded, prepareToJump, jumping, falling, landing }

    Rigidbody2D rig;
    Animator anim;

    [Header("Move")]
    public float speedMovement;
    public float jumpForce;
    public JumpState jumpState = JumpState.grounded;

    [Header("Timers")]
    public float shootTimer;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        Shooting();
        Move();
        Debug.Log(grounded);
        /*Debug.Log(velocity.y);*/
        Jumping();

        anim.SetBool("grounded", grounded);
        anim.SetFloat("velocityY", velocity.y);
    }

    void Shooting()
    {
        if (Input.GetAxis("Fire1") != 0)
            StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        anim.SetBool("isShooting", true);
        Debug.Log("tiro");
        yield return new WaitForSeconds(shootTimer);
        anim.SetBool("isShooting", false);
    }

    void Move()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");

        if (move.x != 0)
        {
            TurnSide(move.x);
        } 
        
        // update properties for moving and animations
        targetVelocity = move * speedMovement;
        anim.SetInteger("velocityX", (int) move.x);
    }

    void TurnSide(float moveX)
    {
        /* Turn the game object to the rigth or left based in the x movement */
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void Jumping()
    {
        if(Input.GetButtonDown("Jump") && anim.GetFloat("velocityY").Equals(0))
        {
            Jump(1);
        }
    }

    void Jump(float jumpMultiplier)
    {
        /*jumpState = JumpState.jumping;*/
        velocity.y = jumpForce * jumpMultiplier;
    }
}
