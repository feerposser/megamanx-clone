using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class PlayerHealthSystem : BaseHealthSystem
{

    public HealthStatusBar healthStatusBar;

    BoxCollider2D collider;
    Rigidbody2D rb;

    private void Awake()
    {
        healthStatusBar.UpdateHealthStatusBar(health);
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
    {
        health -= args.damage;
        healthStatusBar.UpdateHealthStatusBar(health);
        rb.isKinematic = true;
        collider.isTrigger = true;
        //stay untouchble for x seconds while this opacity is higher than normal
    }
}
