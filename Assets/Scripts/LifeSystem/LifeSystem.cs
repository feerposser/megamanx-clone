using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{

    private float health = 100;

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("death");
        }
    }

    public void SetDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
