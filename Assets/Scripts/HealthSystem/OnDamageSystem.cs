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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (damageble)
            {
                if (tagsForDamage.Contains(collision.gameObject.tag))
                {
                    onDamage?.Invoke(this, new DamageEventArgs { damage = 5 });
                }
            }
        }
    }
}