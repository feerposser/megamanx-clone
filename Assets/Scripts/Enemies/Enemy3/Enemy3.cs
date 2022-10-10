using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    /*
     * PREPARETOAPROACH: UPDATE PLAYER LAST POSITION
     * APROACH: GOING INTO THE PLAYER POSITION
     * PREPARETOSTOP: AFTER APROACHSTATE, STOP TO PREPARE TO ATTACK
     * STOP: STOP MOVING
     * PREPAREATTACK: GET THE ACTUAL PLAYER POSITION TO ATTACK
     * ATTACK: ATTACK THE PLAYER LAST POSITION
     * PREPARETOREPOSITION: SET A FIXED DESTINATION TO REPOSITION
     * REPOSITION: AFTER ATTACKING, MOVE WAY AND START ALL OVER AGAIN   
     */
    public enum EnemyState { PREPARETOAPROACH, APROACH, PREPARETOSTOP, STOP, PREPAREATTACK, ATTACK, PREPARETOREPOSITION, REPOSITION  }

    Rigidbody2D rb;
    Transform playerPosition;
    Vector2 lastPlayerPosition;
    Vector2 lastEnemyPosition;
    Vector2 moveWayPosition;

    [SerializeField] EnemyState enemyState;
    [SerializeField] float speed;
    [SerializeField] float stopDistance1;
    [SerializeField] float stopDistance2;
    [SerializeField] float stopTime;

    void Start()
    {
        enemyState = EnemyState.PREPARETOAPROACH;
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (enemyState.Equals(EnemyState.PREPARETOAPROACH))
        {
            UpdateLastPlayerPosition();
            enemyState = EnemyState.APROACH;
        }

        if (enemyState.Equals(EnemyState.APROACH))
            if (StopDistance() == 1) 
                enemyState = EnemyState.PREPARETOSTOP;

        if (enemyState.Equals(EnemyState.PREPAREATTACK))
        {
            UpdateLastPlayerPosition();
            UpdateLastEnemyPosition();
        }

        if (enemyState.Equals(EnemyState.PREPARETOSTOP))
            ComputePrepareToStop();

        if (enemyState.Equals(EnemyState.PREPARETOREPOSITION))
        {
            moveWayPosition = lastPlayerPosition + new Vector2(0, 3);
            enemyState = EnemyState.REPOSITION;
        }

        if (enemyState.Equals(EnemyState.REPOSITION))
            if (Vector2.Distance(transform.position, moveWayPosition) < 0.5f)
                StartCoroutine(Stop());
    }

    private void FixedUpdate()
    {
        if (enemyState.Equals(EnemyState.APROACH)) 
        {
            Vector2 position = new Vector2(lastPlayerPosition.x, lastPlayerPosition.y + 1);
            MoveToPosition(position - (Vector2)transform.position);
        }
        
        if (enemyState.Equals(EnemyState.PREPARETOSTOP))
            MoveToPosition(playerPosition.position - transform.position, 0.7f);

        if (enemyState.Equals(EnemyState.ATTACK))
            MoveToPosition((lastPlayerPosition - lastEnemyPosition).normalized, multiplier: 3);
        
        if (enemyState.Equals(EnemyState.REPOSITION))
            MoveToPosition(moveWayPosition - (Vector2)transform.position, multiplier: 3);
    }

    private void ComputePrepareToStop()
    {
        if (StopDistance() == 2)
        {
            enemyState = EnemyState.STOP;
            StartCoroutine(Stop());
        }
    }
 
    private IEnumerator Stop()
    {
        rb.velocity = Vector2.zero;
        enemyState = EnemyState.PREPAREATTACK;
        yield return new WaitForSeconds(stopTime);
        enemyState = EnemyState.ATTACK;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) enemyState = EnemyState.PREPARETOREPOSITION;
    }

    private void MoveToPosition(Vector2 position, float multiplier=1)
    {
        rb.velocity = position * multiplier;
    }

    private int StopDistance()
    {
        float distance = Vector2.Distance(playerPosition.position, transform.position);

        if (distance < stopDistance2) return 2;
        else if (distance < stopDistance1) return 1;

        return 0;
    }

    private void UpdateLastPlayerPosition() 
    {
        lastPlayerPosition = playerPosition.position;
    }

    private void UpdateLastEnemyPosition()
    {
        lastEnemyPosition = transform.position;
    }
}
