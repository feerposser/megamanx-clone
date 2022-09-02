using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeSystem;

public abstract class EnemyHeathSystem : BaseHealthSystem
{    
    public override void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
    {
        DecreaseHealth(args.damage);
    }
  
}
