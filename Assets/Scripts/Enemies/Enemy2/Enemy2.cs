using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public enum EnemyState { FLYING, PREPARETOBOMBING, BOMBING }

    Rigidbody2D rb;
    Animator anim;
    Transform playerPosition;
    EnemyState enemyState;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform bombSpawn;
    [SerializeField] float speed;

    void Start()
    {
        enemyState = EnemyState.FLYING;
        playerPosition = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (enemyState.Equals(EnemyState.FLYING))
        {
            Move(Vector2.left);
        }
        IntoThePlayerPosition();
    }

    void IntoThePlayerPosition()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position - new Vector3(0, 3, 0), new Vector2(5, 5), 0, playerLayer);

        if (collider)
        {
            DeployBomb();
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
