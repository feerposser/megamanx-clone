using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public abstract class BaseHealthSystem : MonoBehaviour
    {
        [SerializeField] OnDamageSystem onDamage;

        [SerializeField] protected int health = 100;

        private void Start()
        {
            onDamage = GetComponent<OnDamageSystem>();
            onDamage.onDamage += OnDamage;
        }

        protected abstract void Death();

        protected abstract void ExecuteDamage();

        protected void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
        {
            DecreaseHealth(args.damage);
            ExecuteDamage();
        }

        protected void DecreaseHealth(int decreaseValue)
        {
            health -= decreaseValue;
        }
    }
}
