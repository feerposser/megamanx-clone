using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : MonoBehaviour
{
    public enum EnemyState { IDLE, PREPARETORUNNING, RUNNING, PREPARETOCRUSH, CRUSHING, BACKTOIDLE }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(positionToMove, 0.3f);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, -1 * 6));
    }

    private void Start()
    {
        isLeft = true;
        isRunning = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyState = EnemyState.PREPARETORUNNING;

        crusher = transform.GetChild(0).GetComponent<Enemy7Crusher>();
    }

    private void Update()
    {
        if (enemyState.Equals(EnemyState.PREPARETORUNNING))
            PrepareToRunning();
        else if (enemyState.Equals(EnemyState.PREPARETOCRUSH))
            PrepareToCrush();

        anim.SetBool("isRunning", isRunning);
    }


    private void FixedUpdate()
    {
        if (enemyState.Equals(EnemyState.RUNNING))
            Running();
        else if (enemyState.Equals(EnemyState.CRUSHING))
            Crushing();
    }

    /* -- movement -- */
    private void PrepareToRunning()
    {
        float newRunningDistance = isLeft ? -runningDistance : runningDistance;
        positionToMove = transform.position + new Vector3(newRunningDistance, 0, 0);
        enemyState = EnemyState.RUNNING;
    }

    private void Running()
    {
        isRunning = true;

        if (!IsInPositionGoal())
            Move();
        else
            Turning();

        if (IsBreakableBelow())
            enemyState = EnemyState.PREPARETOCRUSH;
    }

    private bool IsBreakableBelow()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 6, breakableGround);
    }

    private bool IsInPositionGoal()
    {
        return Vector2.Distance(transform.position, positionToMove) < 0.5f;
    }

    private void Move()
    {
        rb.velocity = isLeft ? Vector2.left : Vector2.right * speed;
    }

    private void Turning()
    {
        isLeft = !isLeft;
        transform.eulerAngles = new Vector2(0, isLeft ? 0 : 180);

        PrepareToRunning();
    }

    /* -- crush -- */
    private void PrepareToCrush()
    {
        isRunning = false;
        enemyState = EnemyState.CRUSHING;
        crusher.Crush();
    }

    private void Crushing()
    {
        rb.velocity = Vector2.zero;
    }

    public void DidTheCrush()
    {
        enemyState = EnemyState.PREPARETORUNNING;
    }    
}
