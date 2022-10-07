using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    /*
    APROACH: GOING INTO THE PLAYER POSITION
    PREPARETOSTOP: AFTER APROACHSTATE, STOP TO PREPARE TO ATTACK
    STOP: STOP MOVING
    PREPAREATTACK: GET THE ACTUAL PLAYER POSITION TO ATTACK
    ATTACK: ATTACK THE PLAYER LAST POSITION
    REPOSITION: AFTER ATTACKING, MOVE WAY AND START ALL OVER AGAIN
     */
    public enum EnemyState { PREPARETOAPROACH, APROACH, PREPARETOSTOP, STOP, PREPAREATTACK, ATTACK, REPOSITION  }

    Rigidbody2D rb;

    Transform playerPosition;
    Vector2 lastPlayerPosition;

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
        if (enemyState.Equals(EnemyState.PREPARETOAPROACH)) ComputePrepareToAproach();

        if (enemyState.Equals(EnemyState.APROACH)) ComputeAproach();

        if (enemyState.Equals(EnemyState.PREPAREATTACK)) ComputePrepareToAttack();

        if (enemyState.Equals(EnemyState.PREPARETOSTOP)) ComputePrepareToStop();

    }

    private void FixedUpdate()
    {
        if (enemyState.Equals(EnemyState.APROACH)) ExecuteAproach();
        
        if (enemyState.Equals(EnemyState.PREPARETOSTOP)) ExecutePrepareToStop();
        
        if (enemyState.Equals(EnemyState.ATTACK)) Attack();
    }

    private void ComputePrepareToAproach() 
    {
        UpdateLastPlayerPosition();
        enemyState = EnemyState.APROACH;
    }

    private void ComputeAproach()
    {
        if (StopDistance() == 1) enemyState = EnemyState.PREPARETOSTOP;
    }

    private void ExecuteAproach()
    {
        Vector2 position = new Vector2(lastPlayerPosition.x, lastPlayerPosition.y + 1);

        MoveToPosition(position - (Vector2) transform.position);
    }

    private void ComputePrepareToStop()
    {
        if (StopDistance() == 2)
        {
            enemyState = EnemyState.STOP;
            StartCoroutine(Stop());
        }
    }

    private void ExecutePrepareToStop()
    {
        MoveToPosition(playerPosition.position - transform.position, 0.7f);
    }
 
    private IEnumerator Stop()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stopTime);
        enemyState = EnemyState.PREPAREATTACK;
    }

    private void ComputePrepareToAttack()
    {
        UpdateLastPlayerPosition();
        enemyState = EnemyState.ATTACK;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if hit the player, move away and start all over again
    }

    private void Attack()
    {
        MoveToPosition(lastPlayerPosition - (Vector2) transform.position);
    }

    private void MoveToPosition(Vector2 position, float multiplier=1)
    {
        rb.velocity = position * multiplier;
    }

    private int StopDistance()
    {
        float distance = Vector2.Distance(playerPosition.position, transform.position);

        if (distance < stopDistance2)
        {
            return 2;
        } else if (distance < stopDistance1)
        {
            return 1;
        }

        return 0;
    }

    private void UpdateLastPlayerPosition() 
    {
        lastPlayerPosition = GetActualPlayerPosition();
    }

    private Vector2 GetActualPlayerPosition()
    {
        return playerPosition.position;
    }
}
