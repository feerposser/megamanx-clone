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

    private void Awake()
    {
        healthStatusBar.UpdateHealthStatusBar(health);
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
        opacityColor = new Color(1, 1, 1, 0);
        defaultColor = new Color(1, 1, 1, 1);
    }

    public override void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
    {
        DecreaseHealth(args.damage);
        CheckHealth();
        healthStatusBar.UpdateHealthStatusBar(health);
        StartCoroutine("DamageEffect");
    }

    IEnumerator DamageEffect()
    {
        anim.SetTrigger("damage");
        rb.isKinematic = true;
        collider.isTrigger = true;

        yield return new WaitForSeconds(0.8f);
        
        collider.isTrigger = false;
        rb.isKinematic = false;

        StartCoroutine("SpriteBlink");
    }

    IEnumerator SpriteBlink()
    {
        bool active = true;
        for(int i=0; i<20; i++)
        {
            active = !active;

            sprite.color = (!active) ? opacityColor : defaultColor;

            yield return new WaitForSeconds(0.05f);
        }
    }

    public override void Death()
    {
        GameOver();
    }

    public void IncreaseHealth(int increaseValue)
    {
        health += increaseValue;
    }

    private void CheckHealth()
    {
        if (health <= 0) Death();
    }

    private void GameOver()
    {
        Debug.Log("game over");
        Time.timeScale = 0;
    }
}
