using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public class OnDamageSystem : MonoBehaviour
    {
        [SerializeField] List<string> tagsForDamage; /*Tags enabled to set damage to the object*/

        /* Event handler to broadcast damage */
        public class DamageEventArgs : EventArgs { public float damage; }
        public EventHandler<DamageEventArgs> onDamage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (tagsForDamage.Contains(collision.gameObject.tag))
            {
                onDamage?.Invoke(this, new DamageEventArgs { damage = 10 });
            }
        }
    }
}