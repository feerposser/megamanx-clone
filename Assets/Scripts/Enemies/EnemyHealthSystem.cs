using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeSystem;

namespace LifeSystem
{
    public abstract class EnemyHealthSystem : BaseHealthSystem
    {
        [SerializeField] GameObject explosion;

        [SerializeField] Blink blink;

        protected override void ExecuteDamage()
        {
            CheckHealth();
            blink.PlayBlink();
        }

        protected override void Death()
        {
            Instantiate(explosion, transform.position, transform.rotation)
                .GetComponent<DeployExplosion>().Explode();
            Destroy(gameObject);
        }
    }
}
