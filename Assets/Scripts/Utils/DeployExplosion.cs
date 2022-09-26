using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    public void Explode(bool createDamage)
    {
        Explosion gameObj = Instantiate(explosion, transform.position, transform.rotation).GetComponent<Explosion>();
        gameObj.createDamage = createDamage;
        Destroy(gameObject);
    }

    public void Explosions()
    {
        throw new UnityException("Not implemented");
    }
}
