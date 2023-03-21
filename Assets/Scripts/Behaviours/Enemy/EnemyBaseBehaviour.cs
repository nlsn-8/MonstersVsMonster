using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

using Demo.Behaviours;
using Demo.Managers;

namespace Demo.Behaviours.Enemy
{
    public class EnemyBaseBehaviour : MonoBehaviour, IDamageable
    {
        public float Resistance{get;set;}
        public float Health{get;set;}

        protected int _strength;

        public GameObject EnemyDeathAnimation;
        protected IDamageable _damageable;
        protected Random _random;

        protected AudioSource _audioSource;
        protected int _minWaitBetweenPlays = 4;
        protected int _maxWaitBetweenPlays = 10;
        protected float _waitTimeCountdown;
        public string MonsterSound;
        
        protected void Start()
        {
            Init();
            _random = new Random();
            _waitTimeCountdown = _random.Next(3,5);
            _audioSource = SoundManager.Instance.GetSource(MonsterSound);
        }

        protected virtual void Init()
        {
            Health = 100;
            Resistance = 0.8f;
            _strength = 20;
        }

        protected virtual void Update()
        {
            PlayMonsterSoundRandomly();
        }

        protected void PlayMonsterSoundRandomly()
        {
            if(_audioSource == null) return;
            if (!_audioSource.isPlaying)
            {
                if (_waitTimeCountdown < 0f)
                {
                    SoundManager.Instance.Play(MonsterSound);
                    _waitTimeCountdown = _random.Next(_minWaitBetweenPlays, _maxWaitBetweenPlays);
                }
                else
                {
                    _waitTimeCountdown -= Time.deltaTime;
                }
            }
        }

        public virtual void Damage(int damage)
        {
            var dmg = damage * Resistance;
            Health -= dmg;
            // play hit sound
            SoundManager.Instance.Play("Hit");

            if(Health <= 0)
            {
                Die();
                return;
            }
            // play damage animation
        }

        protected virtual void Die()
        {
            // play death sound
            SoundManager.Instance.Play("EnemyDeath");
            // play death animation
            Instantiate(EnemyDeathAnimation, transform.position, Quaternion.identity);
            // Update death counter
            GameManager.Instance.AddToCount(1);
            Destroy(this.gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            _damageable = other.GetComponent<IDamageable>();
            if(_damageable != null)
            {
                SoundManager.Instance.Play("Bite");
                // Enemy makes damage to the other damageable
                // Enemy cannot make damage to other enemies since 
                // they ignore Enemy layer in the enemy collision matrix
                _damageable.Damage(_strength);
                // Enemy takes damage if it collides with a damageable obj
                // Damage(_strength);
            }
        }
    }
}
