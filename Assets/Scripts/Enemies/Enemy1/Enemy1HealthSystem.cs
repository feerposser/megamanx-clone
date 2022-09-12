using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class Enemy1HealthSystem : EnemyHealthSystem
{
    public Blink blink;

    protected override void CheckHealth()
    {
        Debug.Log("bb");
    }

    protected override void Death()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExecuteDamage()
    {
        blink.PlayBlink();
    }
}
