using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] List<string> tags;
    LifeSystem health;

    private void Start()
    {
        health = GetComponent<LifeSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tags.Contains(collision.gameObject.tag)) {
            health.SetDamage(10);
        }
    }

}
