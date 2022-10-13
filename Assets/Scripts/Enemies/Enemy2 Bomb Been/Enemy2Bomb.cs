using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float timeToExplode;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Deploy()
    {
        anim.SetTrigger("deployed");
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(timeToExplode);
        Explode();
    }

    private void Explode()
    {
        DeployExplosion explosionDeployed = Instantiate(explosion, transform.position, Quaternion.identity).GetComponent<DeployExplosion>();
        explosionDeployed.Explode(createDamage: true);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Deploy();
    }
}
