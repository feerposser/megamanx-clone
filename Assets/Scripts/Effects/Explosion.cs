using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    CircleCollider2D damageCollider;

    public bool createDamage;

    [SerializeField] float timeToExplode = .3f;

    void Start()
    {
        damageCollider = GetComponent<CircleCollider2D>();
        damageCollider.enabled = false;
        if (createDamage) damageCollider.enabled = true;
        
        Destroy(gameObject, timeToExplode);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
