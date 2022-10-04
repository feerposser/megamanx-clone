using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class Enemy1HealthSystem : EnemyHealthSystem
{
    Animator anim;
    Enemy1 enemy1;

    [SerializeField] float deathTime = 1.5f;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        enemy1 = gameObject.GetComponent<Enemy1>();
    }

    protected override void CheckHealth()
    {
        Debug.Log(health);
        if (health <= 50)
        {
            StartCoroutine("PrepareDeath");
        }
    }

    IEnumerator PrepareDeath()
    {
        anim.SetTrigger("fall");
        enemy1.speed = 0;
        yield return new WaitForSeconds(deathTime);
        Death();
    }
}
