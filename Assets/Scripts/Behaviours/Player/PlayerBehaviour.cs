using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Managers;

namespace Demo.Behaviours.Player
{
    public class PlayerBehaviour : MonoBehaviour, IDamageable
    {
        public float Health {get;set;}
        public float Resistance {get;set;}

        public GameObject DeathAnimation;
        public HealthBar healthBar;

        private void Start()
        {
            Health = 100;
            Resistance = 0.8f;
            healthBar.SetMaxHealth((int)Health);
        }

        public void Damage(int damage)
        {
            var dmg = damage * Resistance;
            Health -= dmg;
            
            // play hit sound
            SoundManager.Instance.Play("Hit");
            healthBar.SetHealth((int)Health);
            if(Health <= 0)
            {
                Die();
                return;
            }
            // play damage animation
        }

        void Die()
        {
            // play death sound
            SoundManager.Instance.Play("EnemyDeath");
            // play death animation
            Instantiate(DeathAnimation, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
