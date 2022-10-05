using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeSystem;

namespace LifeSystem
{
    public class EnemyHealthSystem : BaseHealthSystem
    {
        [SerializeField] GameObject deployExplosion;

        [SerializeField] Blink blink;

        protected override void ExecuteDamage()
        {
            CheckHealth();
            blink.PlayBlink();
        }

        protected override void Death()
        {
            Instantiate(deployExplosion, transform.position, transform.rotation)
                .GetComponent<DeployExplosion>().Explode(createDamage: false);
            Destroy(gameObject);
        }

        protected override void CheckHealth()
        {
            if (health <= 0)
            {
                Death();
            }
        }
    }
}
