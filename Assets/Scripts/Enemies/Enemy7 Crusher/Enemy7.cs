using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : MonoBehaviour
{
    public enum EnemyState { IDLE, PREPARETORUNNING, RUNNING, PREPARETOCRUSH, CRUSH, BACKTOIDLE }

    Animator anim;
    bool isRunning;
    Rigidbody2D rb;
    Vector2 positionToMove;

    [SerializeField] bool isLeft;
    [SerializeField] float speed;
    [SerializeField] float runningDistance;
    [SerializeField] EnemyState enemyState;

    private void Start()
    {
        isLeft = true;
        isRunning = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyState = EnemyState.PREPARETORUNNING;
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
        if (enemyState.Equals(EnemyState.RUNNING))
        {
            isRunning = true;
            if (!IsInPositionGoal())
            {
                Running();
            } else
            {
                isLeft = !isLeft;
                PrepareToRunning();
            }
        }
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
