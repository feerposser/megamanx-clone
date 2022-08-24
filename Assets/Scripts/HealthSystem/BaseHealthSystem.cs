using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public class BaseHealthSystem : MonoBehaviour
    {
        OnDamageSystem onDamage;

        [SerializeField] protected float health = 100;

        private void Start()
        {
            onDamage = GetComponent<OnDamageSystem>();
            onDamage.onDamage += OnDamage;
        }

        public void OnDamage(object sender, OnDamageSystem.DamageEventArgs args)
        {
            Debug.Log(args.damage);
            health -= args.damage;
        }
    }

}
