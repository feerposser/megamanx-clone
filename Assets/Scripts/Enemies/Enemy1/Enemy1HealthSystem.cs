using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class Enemy1HealthSystem : EnemyHealthSystem
{
    Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    protected override void CheckHealth()
    {
        Debug.Log(health);
        if (health <= 50)
        {
            //Death();
            anim.SetTrigger("fall");
        }
    }

    public void AquiEuAlteroOsColisores()
    {
        Debug.Log("111111111111111111");
    }    
}
