using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public abstract class BaseHealthSystem : MonoBehaviour
    {

        [SerializeField] protected float health = 100;

        public abstract void ExecuteDamageAnimation();

        // death handdler
        protected abstract void CheckHealth();

        public void SetDamage(float damage)
        {
            health -= damage;
            CheckHealth();
            ExecuteDamageAnimation();
        }
    }

}
