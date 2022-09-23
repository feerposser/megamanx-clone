using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public enum EnemyState { FLYING, MOVETOPLAYERPOSITION, PREPARETOBOMBING, BOMBING, MOVEWAY }

    Rigidbody2D rb;
    Animator anim;
    Transform playerPosition;
    EnemyState enemyState;

    float playerLastDistance;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform bombSpawn;
    [SerializeField] float speed;
    [SerializeField] bool bombing;

    void Start()
    {
        bombing = false;
        playerLastDistance = float.MaxValue;
        enemyState = EnemyState.FLYING;
        playerPosition = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (enemyState.Equals(EnemyState.MOVEWAY))
        {
            Move(Vector2.left);
            // after that, just up and destroy
        } 
        else if (enemyState.Equals(EnemyState.FLYING))
        {
            Move(Vector2.left);
            PlayerPositionDetection();
        } 
        else if (enemyState.Equals(EnemyState.MOVETOPLAYERPOSITION))
        {
            if (GetXDistance() > 1)
            {
                Debug.Log(GetXDistance());
                Move(Vector2.left);
            } 
            else
            {
                float playerDistance = Vector2.Distance(transform.position, playerPosition.position);
                if (playerDistance < playerLastDistance)
                {
                    Move(Vector2.left);
                } else
                {
                    enemyState = EnemyState.PREPARETOBOMBING;
                }
                playerLastDistance = playerDistance;
            }
        } 
        else if (enemyState.Equals(EnemyState.PREPARETOBOMBING))
        {
            DeployBomb();
            enemyState = EnemyState.MOVEWAY;
        }
        anim.SetBool("bombing", bombing);
    }

    private float GetXDistance()
    {
        float x = playerPosition.position.x - transform.position.x;
        return x < 0 ? -x : x;
    }

    void PlayerPositionDetection()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position - new Vector3(0, 3, 0), new Vector2(5, 5), 0, playerLayer);

        if (collider)
        {
            enemyState = EnemyState.MOVETOPLAYERPOSITION;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, 3, 0), new Vector2(5, 5));
    }

    void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    void DeployBomb()
    {
        Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
    }
}
