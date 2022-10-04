using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public abstract class BaseHealthSystem : MonoBehaviour
    {
        public class DamageArgs
        {
            public int damage { get; set; }
        }

        protected DamageArgs damageArgs;

        [SerializeField] OnDamageSystem onDamage;

        [SerializeField] protected int health = 100;
        [SerializeField] protected float decreaseHealthMultiplier;

        private void Start()
        {
            onDamage = GetComponent<OnDamageSystem>();
            onDamage.onDamage += OnDamage;

            damageArgs = new DamageArgs();
        }

        protected abstract void Death();

        protected abstract void ExecuteDamage();

        protected abstract void CheckHealth();

        protected void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
        {
            damageArgs.damage = args.damage;
            DecreaseHealth(args.damage);
            ExecuteDamage();
        }

        protected void DecreaseHealth(int decreaseValue)
        {
            health -= (int) (decreaseValue * decreaseHealthMultiplier);
        }

        private void OnDestroy()
        {
            onDamage.onDamage -= OnDamage;
        }
    }
}
