using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Behaviours;

namespace Demo.Behaviours.Enemy
{
    public class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        public float Health{get;set;}
        public float Resistance{get;set;}
        
        private void Start()
        {
            Health = 100;
            Resistance = 0.8f;
        }

        public void Damage(int damage)
        {
            var dmg = damage * Resistance;
            Health -= dmg;
            if(Health <= 0)
            {
                Die();
                return;
            }
            // play damage animation
        }

        public void Die()
        {
            Destroy(this.gameObject);
            // play death animation
        }

    }
}
