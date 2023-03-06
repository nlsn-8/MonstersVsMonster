using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Demo.Behaviours;
using Demo.Managers;

namespace Demo.Behaviours.Enemy
{
    public class EnemyBehaviour : MonoBehaviour, IDamageable
    {
        public GameObject EnemyDeathAnimation;
        private IDamageable _damageable;
        private Random _random;
        public float Health{get;set;}
        public float Resistance{get;set;}
        private int _strength;
        private AudioSource _audioSource;
        private int _minWaitBetweenPlays = 4;
        private int _maxWaitBetweenPlays = 10;
        private float _waitTimeCountdown;
        
        private void Start()
        {
            Health = 100;
            Resistance = 0.8f;
            _strength = 20;
            _random = new Random();
            _audioSource = SoundManager.Instance.GetSource("Monster");
            _waitTimeCountdown = _random.Next(3,5);
        }

        private void Update()
        {
            PlayMonsterSoundRandomly();
        }

        void PlayMonsterSoundRandomly()
        {
            if (!_audioSource.isPlaying)
            {
                if (_waitTimeCountdown < 0f)
                {
                    // currentClip = audioClips[Random.Range(0, audioClips.Count)];
                    // source.clip = currentClip;
                    _audioSource.Play();
                    _waitTimeCountdown = _random.Next(_minWaitBetweenPlays, _maxWaitBetweenPlays);
                }
                else
                {
                    _waitTimeCountdown -= Time.deltaTime;
                }
            }
        }

        public void Damage(int damage)
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

        private void Die()
        {
            // play death sound
            SoundManager.Instance.Play("EnemyDeath");
            // play death animation
            Instantiate(EnemyDeathAnimation, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Enemy")) return;
            _damageable = other.GetComponent<IDamageable>();
            if(_damageable != null)
            {
                SoundManager.Instance.Play("Bite");
                // Enemy makes damage to the other damageable
                _damageable.Damage(_strength);
                // Enemy takes damage if it collides with a damageable obj
                // Damage(_strength);
            }
        }



    }
}
