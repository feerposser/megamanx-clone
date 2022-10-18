using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : MonoBehaviour
{
    public enum EnemyState { IDLE, PREPARETORUNNING, RUNNING, PREPARETOCRUSH, CRUSH, BACKTOIDLE }

    Animator anim;
    bool isRunning;
    Rigidbody2D rb;
    Enemy7Crusher crusher;
    Vector2 positionToMove;

    [SerializeField] bool isLeft;
    [SerializeField] float speed;
    [SerializeField] float runningDistance;
    [SerializeField] EnemyState enemyState;
    [SerializeField] LayerMask breakableGround;

    private void Start()
    {
        
        isLeft = true;
        isRunning = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyState = EnemyState.PREPARETORUNNING;

        crusher = transform.GetChild(0).GetComponent<Enemy7Crusher>();
        crusher.enabled = false;
    }

    private void Update()
    {
        if (enemyState.Equals(EnemyState.PREPARETORUNNING))
        {
            PrepareToRunning();
        }

        anim.SetBool("isRunning", isRunning);
    }

    private void FixedUpdate()
    {
        IsBreakableBelow();
        if (enemyState.Equals(EnemyState.RUNNING))
        {
            isRunning = true;
            if (!IsInPositionGoal())
            {
                Running();
            } 
            else
            {
                isLeft = !isLeft;
                PrepareToRunning();
            }
        }
    }

    private bool IsBreakableBelow()
    {
        bool response = Physics2D.Raycast(transform.position, Vector2.down, 6, breakableGround);
        Debug.DrawRay(transform.position, new Vector2(0, -1 * 6), Color.red);

        if (response)
        {
            Debug.Log("Breakable ground detected!!!");
        }

        return response;
    }

    private bool IsInPositionGoal()
    {
        bool response = false;
        if (Vector2.Distance(transform.position, positionToMove) < 0.5f)
            response = true;
        return response;
    }

    private void PrepareToRunning()
    {
        float newRunningDistance = isLeft ? -runningDistance : runningDistance;
        positionToMove = transform.position + new Vector3(newRunningDistance, 0, 0);
        enemyState = EnemyState.RUNNING;
    }

    private void Running()
    {
        rb.velocity = positionToMove.normalized * speed;
    }

}
