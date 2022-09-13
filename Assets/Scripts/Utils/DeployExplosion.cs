using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    public void Explosions(int numberOfExplosions)
    {
        throw new UnityException("Not implemented");
    }
}
