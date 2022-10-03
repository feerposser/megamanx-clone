using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy8 : MonoBehaviour
{

    public enum EnemyState { DEFAULT, PREPARETOMOVE, MOVE, PREPARETOTURN, TURN }

    Rigidbody2D rb;
    Animator anim;

    [SerializeField] EnemyState enemyState;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float speed;
    [SerializeField] bool isLeft;

    void Start()
    {
        enemyState = EnemyState.MOVE;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ComputeMove();
        ComputeTurn();
        IsInBottomEdge();
    }

    void FixedUpdate()
    {
        ExecuteMove();
    }

    void ComputeTurn()
    {
        if ((WallContact() || IsInBottomEdge()) && enemyState.Equals(EnemyState.MOVE))
        {
            enemyState = EnemyState.PREPARETOTURN;
            StartCoroutine("Turn");
        }
    }

    IEnumerator Turn()
    {
        enemyState = EnemyState.TURN;

        transform.eulerAngles = isLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
        isLeft = !isLeft;
        anim.SetTrigger("turn");

        yield return new WaitForSeconds(0.3f);

        enemyState = EnemyState.PREPARETOMOVE;
    }

    void ComputeMove()
    {
        if (enemyState.Equals(EnemyState.PREPARETOMOVE))
        {
            enemyState = EnemyState.MOVE;
        }
    }

    void ExecuteMove()
    {
        if (enemyState.Equals(EnemyState.MOVE))
        {
            if (isLeft)
            {
                Move(Vector2.left);
            }
            else
            {
                Move(Vector2.right);
            }
        }
    }

    bool WallContact()
    {
        if (enemyState.Equals(EnemyState.MOVE))
        {
            Vector2 rayCastDirection = GetRaycastDirection();
            return Physics2D.Raycast(transform.position, rayCastDirection, 0.5f, wallLayer);
        }
        return false;
    }

    bool IsInBottomEdge()
    {
        bool colliding = false;
        if (enemyState.Equals(EnemyState.MOVE))
        {
            Vector2 raycastDirection = GetRaycastDirection();
            Vector2 raycastPosition = transform.position + (Vector3)raycastDirection + Vector3.down;
            colliding = Physics2D.Raycast(raycastPosition, raycastDirection, 1, wallLayer);
        }
        return colliding ? false : true;
    }

    Vector2 GetRaycastDirection()
    {
        return isLeft ? Vector2.left : Vector2.right;
    }

    void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.left);
    }
}
