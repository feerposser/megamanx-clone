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

    [SerializeField] float speed;
    [SerializeField] bool bombing;
    [SerializeField] float bombingTime;
    [SerializeField] Transform bombSpawn;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject bombPrefab;

    void Start()
    {
        bombing = false;
        enemyState = EnemyState.FLYING;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerLastDistance = float.MaxValue;
        playerPosition = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        if (enemyState.Equals(EnemyState.MOVEWAY))
        {
            Move(Vector2.left);
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
            enemyState = EnemyState.BOMBING;
            bombing = true;
            StartCoroutine("Bombing");
        }
        else if (enemyState.Equals(EnemyState.BOMBING))
        {
            Move(Vector2.zero);
        }
        anim.SetBool("bombing", bombing);
    }

    IEnumerator Bombing()
    {
        yield return new WaitForSeconds(bombingTime);
        DeployBomb(Vector2.zero);

        Vector2 direction = (transform.position.x > playerPosition.position.x) ? Vector2.left : Vector2.right;

        yield return new WaitForSeconds(bombingTime);
        DeployBomb(direction / 2f);

        yield return new WaitForSeconds(bombingTime);
        DeployBomb(direction / 0.7f);

        yield return new WaitForSeconds(bombingTime / 2);

        bombing = false;
        enemyState = EnemyState.MOVEWAY;
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

    void DeployBomb(Vector2 direction)
    {
        Rigidbody2D go = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        go.velocity = direction * 2;
    }
}
