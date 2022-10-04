using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class Enemy1HealthSystem : EnemyHealthSystem
{
    public enum HealthMode { ALIVE, PREPARETODEATH, DEATH }

    Animator anim;
    Enemy1 enemy1;
    float deathTimer;
    float stopSpeed;

    [SerializeField] HealthMode healthMode;

    private void Awake()
    {
        healthMode = HealthMode.ALIVE;
        anim = gameObject.GetComponent<Animator>();
        enemy1 = gameObject.GetComponent<Enemy1>();
        stopSpeed = enemy1.speed * .3f;
    }

    private void Update()
    {
        if (healthMode.Equals(HealthMode.PREPARETODEATH))
        {
            if (stopSpeed > enemy1.speed)
            {
                enemy1.speed = 0;
            }

            if (enemy1.speed < 1.5f)
            {
                healthMode = HealthMode.DEATH;
            }

            if (deathTimer + 1 < Time.time)
            {
                enemy1.speed = enemy1.speed - (enemy1.speed * 0.5f);
                deathTimer = Time.time;
            }
        }

        if (healthMode.Equals(HealthMode.DEATH)) Death();
    }


    protected override void CheckHealth()
    {
        if (healthMode.Equals(HealthMode.PREPARETODEATH))
        {
            DecreaseHealth(Mathf.RoundToInt(damageArgs.damage * 0.3f));
        }

        if (health <= 0)
        {
            healthMode = HealthMode.DEATH;
        }
        else if (health <= 50)
        {
            StartCoroutine("PrepareDeath");
        }

        Debug.Log(health);
    }

    IEnumerator PrepareDeath()
    {
        anim.SetTrigger("fall");
        healthMode = HealthMode.PREPARETODEATH;
        yield return new WaitForSeconds(1.5f);
        deathTimer = Time.time;
    }
}
