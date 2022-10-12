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
                StartCoroutine(Shoot(EndShoting));
        }

        anim.SetInteger("state", state);
    }

    private void EndShoting()
    {
        enemyState = EnemyState.IDLE;
        state = 3;
        idleTimer = Time.time + idleTime;
    }

    private IEnumerator Shoot(Action callback)
    {
        isShooting = true;
        int shotType = UnityEngine.Random.Range(0, shots.Length);
        yield return new WaitForSeconds(1.5f);

        Instantiate(shots[1], launchPosition1.position, Quaternion.identity);
        //Instantiate(shots[shotType], launchPosition2.position, Quaternion.identity);

        yield return new WaitForSeconds(2);
        isShooting = false;

        callback?.Invoke();
    }

}
