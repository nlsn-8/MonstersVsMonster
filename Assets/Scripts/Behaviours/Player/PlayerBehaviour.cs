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

        private float _invulnerabilityDuration = 1f;
        private int _numberOfFlashes = 3;
        public SpriteRenderer PlayerSprite;

        public GameObject DeathAnimation;
        private GameObject _healthbarObj; 
        private HealthBar _healthBar; 

        private void Start()
        {
            Health = 100;
            Resistance = 0.8f;
            _healthbarObj = GameObject.Find("HealthBar");
            if(_healthbarObj == null ) return;
            _healthBar = _healthbarObj.GetComponent<HealthBar>();
            _healthBar.SetMaxHealth((int)Health);
        }

        public void Damage(int damage)
        {
            var dmg = damage * Resistance;
            Health -= dmg;
            
            // play hit sound
            SoundManager.Instance.Play("Hit");
            _healthBar.SetHealth((int)Health);
            if(Health <= 0)
            {
                Die();
                return;
            }
            // play damage animation
            StartCoroutine(Invulnerability());
        }

        private IEnumerator Invulnerability()
        {
            Physics2D.IgnoreLayerCollision(9,8,true);
            for(int i = 0; i<_numberOfFlashes;i++)
            {
                PlayerSprite.color = new Color(1,0,0,0.5f);
                yield return new WaitForSeconds(_invulnerabilityDuration/_numberOfFlashes);
                PlayerSprite.color = Color.white;
                yield return new WaitForSeconds(_invulnerabilityDuration/_numberOfFlashes);
            }
            Physics2D.IgnoreLayerCollision(9,8,false);
        }

        void Die()
        {
            GameManager.Instance.GameFinished();
            // play death sound
            SoundManager.Instance.Play("EnemyDeath");
            // play death animation
            Instantiate(DeathAnimation, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
