using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeSystem;

public class PlayerLifeSystem : BaseHealthSystem
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void ExecuteDamageAnimation()
    {
        anim.SetTrigger("damage");
    }

    protected override void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.Log("death system");
        }
    }
}
