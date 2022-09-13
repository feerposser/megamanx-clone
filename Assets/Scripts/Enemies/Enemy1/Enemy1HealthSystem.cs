using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class Enemy1HealthSystem : EnemyHealthSystem
{
    Animator anim;
    
    public Blink blink;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    protected override void CheckHealth()
    {
        Debug.Log(health);
        if (health <= 20)
        {
            Death();
        }
    }

    protected override void Death()
    {
        Debug.Log("morreu");
        Destroy(gameObject);
    }

    protected override void ExecuteDamage()
    {
        CheckHealth();
        blink.PlayBlink();
    }
}
