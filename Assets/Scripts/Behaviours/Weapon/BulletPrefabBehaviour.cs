using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Behaviours;

namespace Demo.Behaviours.Weapon
{
    public class BulletPrefabBehaviour : MonoBehaviour
    {
        private IDamageable _damageable;
        private static float _playerSpeed;
        public static float PlayerBulletSpeed // player movement can change this value
        {
            set
            {
                _playerSpeed = value;
            }
        }
        private float _bulletSpeed = 50f;
        public float BulletSpeed // Powerup can change this value
        {
            set
            {
                _bulletSpeed = value;
            }
        }

        public Rigidbody2D BulletRigidbody;

        private void FixedUpdate()
        {
            MoveBullet();
        }

        private void MoveBullet()
        {
            BulletRigidbody.velocity = transform.right * _bulletSpeed;
            DestroyGO();
            // BulletRigidbody.velocity = new Vector2(1+_playerSpeed, 0) * _bulletSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _damageable = other.GetComponent<IDamageable>();
            if(_damageable != null)
            {
                _damageable.Damage();
                DestroyGO(0f);
            }
        }

        private void DestroyGO(float time=3f)
        {
            Destroy(this.gameObject, time);
        }
    }
}
