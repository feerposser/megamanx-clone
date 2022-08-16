using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] protected float health;
    [SerializeField] private float damage;

    void ReceiveDamage(float damage)
    {
        health -= damage;
    }
}
