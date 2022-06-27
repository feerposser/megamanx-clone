using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;

    Vector2 fallVectorGravity;
    Rigidbody2D rb;

    public enum WallslideState { DEFAULT, PREPARETOSLIDE, SLIDING, PREPARETOJUMP, WALLJUMPING }
    public enum JumpState { DEFAULT, PREPARETOJUMP, JUMPING, PREPARETOFALL, FALLING }
    public enum DashState { DEFAULT, PREPARETODASH, DASHING }
    public enum SideState { RIGHT, LEFT }

    [Header("Movement")]
    public SideState sideState = SideState.RIGHT;
    public float xSpeed = 7.5f;
    public float xMoveImput;
    public bool isGrounded;

    [Header("Jump")]
    public JumpState jumpState = JumpState.FALLING;
    public bool isJumping = false;
    public float fallMultiplaier = 5.5f;
    public float jumpMultiplaier = 2;
    public float jumpForce = 7;
    public float jumpCounter;
    public float jumpTime =.35f;

    [Header("Wall Sliding")]
    public WallslideState wallslideState = WallslideState.DEFAULT;
    public float maxWallSliderTime = .3f;
    public float groundCheckDistance = -.5f;
    public float wallSlidingEndsAt = 0;
    public bool isWallSliding = false;
    public bool isWallJumping = false;
    public float wallDistance = .55f;

    [Header("Dash")]
    public DashState dashState = DashState.DEFAULT;
    public bool isDashing = false;
    public float dashTime = 0.19f;
    public float dashSpeed = 4.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallVectorGravity = new Vector2(rb.velocity.x, -Physics2D.gravity.y);
    }

    void Update()
    {
        ComputeMovement(out xMoveImput);

        ComputeJump();

        ComputeWallSliding(xMoveImput);

        ComputeDash();

        if (Input.GetButtonDown("Fire3") && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        } else if (Input.GetButtonDown("Fire3") && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    void FixedUpdate()
    {
        ExecuteMovement(xMoveImput);

        IsGrounded();

        ExecuteJump();

        ExecuteWallSliding();

        ExecuteDash();
    }

    private void IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position + new Vector3(0, -.5f, 0), new Vector3(.97f, .03f, 0), 0, groundLayer);

        isGrounded = collider ? true : false;
    }

    public bool HaveWallContact()
    {
        RaycastHit2D rayWallCheck;

        if (sideState == SideState.RIGHT)
        {
            rayWallCheck = Physics2D.Raycast(transform.position + new Vector3(0, groundCheckDistance, 0), Vector2.right, wallDistance, groundLayer);
            Debug.DrawRay(transform.position + new Vector3(0, groundCheckDistance, 0), Vector2.right, Color.magenta);
        }
        else
        {
            rayWallCheck = Physics2D.Raycast(transform.position + new Vector3(0, groundCheckDistance, 0), Vector2.left, wallDistance, groundLayer);
            Debug.DrawRay(transform.position + new Vector3(0, groundCheckDistance, 0), Vector2.left, Color.magenta);
        }

        return rayWallCheck;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -.5f, 0), new Vector3(.97f, .03f, 0));
    }

    /* --- Start Dash --- */
    private void Dash(float multiplier = 1)
    {
        Vector2 vector = (sideState == SideState.RIGHT) ? 
            Vector2.right * multiplier : Vector2.left * multiplier;

        rb.AddForce(vector * multiplier, ForceMode2D.Impulse);
    }

    private IEnumerator CoroutineExecuteDash()
    {
        isDashing = true;
        Dash(dashSpeed);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    private void ExecuteDash()
    {
        if (dashState == DashState.PREPARETODASH)
        {
            dashState = DashState.DASHING;
            StartCoroutine("CoroutineExecuteDash");
        }
    }

    protected void ComputeDash()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dashState = DashState.PREPARETODASH;
        }
    }

    /* --- Start Wall Sliding --- */
    private void WallSlidingPrepareToSlide()
    {
        wallslideState = WallslideState.SLIDING;

        isJumping = false;
        wallSlidingEndsAt = Time.time + maxWallSliderTime;
    }

    private void WallSlidingSliding()
    {
        isWallSliding = true;
        if (isWallSliding && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
        }

        if (wallSlidingEndsAt < Time.time)
        {
            isWallSliding = false;
            wallSlidingEndsAt = 0;
            wallslideState = WallslideState.DEFAULT;
        }
    }

    private void WallSlidingPrepareToJump()
    {
        isWallJumping = true;
        isWallSliding = false;
        Jump(2);
        wallslideState = WallslideState.WALLJUMPING;
    }

    private void ExecuteWallSliding()
    {
        if (wallslideState == WallslideState.PREPARETOSLIDE)
        {
            WallSlidingPrepareToSlide();
        }

        if (wallslideState == WallslideState.SLIDING)
        {
            WallSlidingSliding();
        }

        if (wallslideState == WallslideState.PREPARETOJUMP)
        {
            WallSlidingPrepareToJump();
        }
    }

    protected void ComputeWallSliding(float move)
    {
        if (!isGrounded && HaveWallContact() && move != 0 && wallslideState == WallslideState.DEFAULT)
        {
            wallslideState = WallslideState.PREPARETOSLIDE; 
        }

        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            wallslideState = WallslideState.PREPARETOJUMP;
        }

        if (Input.GetButtonUp("Jump") && isWallJumping)
        {
            wallslideState = WallslideState.DEFAULT;
        }
    }

    /* --- Start Jump --- */
    private void Jump(float jumpMultiplier = 1)
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce * jumpMultiplier), ForceMode2D.Impulse);
        jumpState = JumpState.JUMPING;
        isJumping = true;
        jumpCounter = 0;
    }

    private void JumpPrepareToFall()
    {
        jumpState = JumpState.FALLING;
        isJumping = false;
        jumpCounter = 0;

        //reduce de y velocity if is going up
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }

    private void JumpJumping()
    {
        jumpCounter += Time.deltaTime;

        if (jumpCounter > jumpTime) isJumping = true;

        float t = jumpCounter / jumpTime;
        float currentMultiplier = jumpMultiplaier;

        if (t > 0.5)
        {
            currentMultiplier = jumpMultiplaier * (1 - t);
        }

        rb.velocity += new Vector2(rb.velocity.x, jumpForce * currentMultiplier) * Time.deltaTime;

        jumpState = (rb.velocity.y < 0) ? JumpState.PREPARETOFALL : JumpState.JUMPING;
    }

    private void JumpFalling()
    {
        // verify if the velocity == 0 and stop falling state. set to defaul
        if (isGrounded)
        {
            jumpState = JumpState.DEFAULT;
        }
        else if (!isWallSliding)
        {
            Falling();
        }
    }

    private void Falling()
    {
        rb.velocity -= fallVectorGravity * fallMultiplaier * Time.deltaTime;
    }

    private void ExecuteJump()
    {
        if (jumpState.Equals(JumpState.PREPARETOJUMP))
        {
            Jump();
        }

        // set falling when fall from a cliff;
        if (jumpState.Equals(JumpState.DEFAULT) && rb.velocity.y < 0)
        {
            jumpState = JumpState.PREPARETOFALL;
        }

        if (jumpState.Equals(JumpState.PREPARETOFALL))
        {
            JumpPrepareToFall();
        }

        // jump highier ou lower depend on jump time
        if (jumpState.Equals(JumpState.JUMPING)) // if (rb.velocity.y > 0 && isJumping)
        {
            JumpJumping();
        }

        // if the character is falling, accelerate the fall
        if (jumpState.Equals(JumpState.FALLING))
        {
            JumpFalling();
        }
    }

    protected void ComputeJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            jumpState = JumpState.PREPARETOJUMP;
        } 

        if (Input.GetButtonUp("Jump"))
        {
            jumpState = JumpState.PREPARETOFALL;
        }
    }

    /* --- Start Move --- */
    private void Move(float move, float moveMultiplier = 1)
    {
        rb.velocity = new Vector2(move * xSpeed * moveMultiplier, rb.velocity.y);
        //Debug.Log("move: " + rb.velocity.y);
    }

    private void ExecuteMovement(float move)
    {
        if(!isDashing)
        {
            Move(move);
        }
    }

    protected void ComputeMovement(out float move)
    {
        move = Input.GetAxis("Horizontal");

        if (move > 0)
        {
            sideState = SideState.RIGHT;
        }
        else if (move < 0)
        {
            sideState = SideState.LEFT;
        }
    }
}
