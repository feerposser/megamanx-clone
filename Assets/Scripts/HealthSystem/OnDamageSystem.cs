using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public class OnDamageSystem : MonoBehaviour
    {
        [SerializeField] List<string> tagsForDamage; /*Tags enabled to set damage to the object*/

        /* Event handler to broadcasting damage */
        public class DamageEventArgs : EventArgs { public int damage; }
        public EventHandler<DamageEventArgs> onDamage;

        public bool damageble = true;

        private void ExecuteDamage(GameObject collision)
        {
            if (damageble)
            {
                if (collision.CompareTag("Damageable"))
                {
                    onDamage?.Invoke(this, new DamageEventArgs { damage = 5 });
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ExecuteDamage(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ExecuteDamage(collision.gameObject);
        }
    }
}