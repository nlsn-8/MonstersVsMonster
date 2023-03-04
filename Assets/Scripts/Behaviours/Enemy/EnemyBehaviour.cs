using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Behaviours;

namespace Demo.Behaviours.Enemy
{
    public class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        private IDamageable _damageable;
        public float Health{get;set;}
        public float Resistance{get;set;}
        private int _strength;
        
        private void Start()
        {
            Health = 100;
            Resistance = 0.8f;
            _strength = 20;
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

        private void Die()
        {
            Destroy(this.gameObject);
            // play death animation
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _damageable = other.GetComponent<IDamageable>();
            if(_damageable != null)
            {
                // Enemy makes damage to the other damageable
                _damageable.Damage(_strength);
                // Enemy takes damage if it collides with a damageable obj
                // Damage(_strength);
            }
        }



    }
}
