using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeSystem
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] List<string> tags;
        BaseHealthSystem health;

        private void Start()
        {
            health = GetComponent<BaseHealthSystem>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (tags.Contains(collision.gameObject.tag))
            {
                health.SetDamage(10);
            }
        }
    }
}