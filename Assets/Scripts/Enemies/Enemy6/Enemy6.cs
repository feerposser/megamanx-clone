using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy6 : MonoBehaviour
{
    public enum EnemyState { IDLE, PREPARETOSHOT, SHOOTING }

    Animator anim;
    float idleTimer;
    float prepareToShotTimer;

    [SerializeField] Transform launchPosition1;
    [SerializeField] Transform launchPosition2;

    [SerializeField] bool isShooting;

    [SerializeField] GameObject[] shots;
    [SerializeField] EnemyState enemyState;
    [SerializeField] int state;
    [SerializeField] float idleTime;
    [SerializeField] float shootingTime;
    [SerializeField] float prepareToShotTime;

    private void Start()
    {
        isShooting = false;
        enemyState = EnemyState.IDLE;
        anim = GetComponent<Animator>();
        idleTimer = Time.time + idleTime;
    }

    private void Update()
    {
        if (enemyState.Equals(EnemyState.IDLE))
        {
            if (idleTimer < Time.time)
            {
                enemyState = EnemyState.PREPARETOSHOT;
                prepareToShotTimer = Time.time + prepareToShotTime;
            }
        } 
        else if (enemyState.Equals(EnemyState.PREPARETOSHOT))
        {
            state = 1;
            if (prepareToShotTimer < Time.time) 
                enemyState = EnemyState.SHOOTING;
        } 
        else if (enemyState.Equals(EnemyState.SHOOTING))
        {
            state = 2;
            if (!isShooting)
                StartShooting();
        }

        anim.SetInteger("state", state);
    }

    private void StartShooting()
    {
        isShooting = true;
        StartCoroutine(Shoot(EndShoting));
    }

    private void EndShoting()
    {
        isShooting = false;
        enemyState = EnemyState.IDLE;
        state = 3;
        idleTimer = Time.time + idleTime;
    }

    private IEnumerator Shoot(Action callback)
    {
        int shootType = UnityEngine.Random.Range(0, shots.Length);
        yield return new WaitForSeconds(1.5f);
        
        if (shootType == 0)
        {
            ShootShock();
        } else if (shootType == 1)
        {
            StartCoroutine(ShootMissile());
        }

        yield return new WaitForSeconds(2);
        callback?.Invoke();
    }

    private IEnumerator ShootMissile()
    {
        Instantiate(shots[1], launchPosition1.position, Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = 1;
        yield return new WaitForSeconds(0.7f);
        Instantiate(shots[1], launchPosition2.position, Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void ShootShock()
    {
        Instantiate(shots[0], launchPosition1.position, Quaternion.identity);
        Instantiate(shots[0], launchPosition2.position, Quaternion.identity);
    }

}
