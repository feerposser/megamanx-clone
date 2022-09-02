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

        public abstract void OnDamage(object sender, OnDamageSystem.DamageEventArgs args);

        public abstract void Death();

        protected void DecreaseHealth(int decreaseValue)
        {
            health -= decreaseValue;
        }
    }
}
