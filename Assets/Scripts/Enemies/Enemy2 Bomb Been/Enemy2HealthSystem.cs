using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeSystem;

public class Enemy2HealthSystem : EnemyHealthSystem
{
    protected override void CheckHealth()
    {
        if (health <= 0)
        {
            Death();
        }
    }
}
