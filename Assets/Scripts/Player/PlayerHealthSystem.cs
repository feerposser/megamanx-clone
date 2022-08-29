using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LifeSystem;

public class PlayerHealthSystem : BaseHealthSystem
{

    public HealthStatusBar healthStatusBar;

    SpriteRenderer sprite;
    Color defaultColor;
    Color opacityColor;

    BoxCollider2D collider;
    Rigidbody2D rb;
    Animator anim;

    bool onDamage;

    private void Awake()
    {
        healthStatusBar.UpdateHealthStatusBar(health);
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
        defaultColor = new Color(1, 1, 1, 1);
        opacityColor = new Color(1, 1, 1, 0.5f);
    }

    public override void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
    {
        health -= args.damage;
        healthStatusBar.UpdateHealthStatusBar(health);
        onDamage = true;
        StartCoroutine("DamageEffect");
        //stay untouchble for x seconds while this opacity is higher than normal
    }

    private void Update()
    {
        if (onDamage)
        {
            sprite.color = opacityColor;
        }
        else
        {
            sprite.color = defaultColor;
        }
    }

    IEnumerator DamageEffect()
    {
        rb.isKinematic = true;
        collider.isTrigger = true;

        // sprite.color
        
        anim.SetTrigger("damage");
        yield return new WaitForSeconds(1.2f);
        
        collider.isTrigger = false;
        rb.isKinematic = false;

        onDamage = false;
    }
}
