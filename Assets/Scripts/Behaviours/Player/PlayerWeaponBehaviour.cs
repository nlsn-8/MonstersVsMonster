using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Behaviours.Weapon;
using Demo.Behaviours;
using Demo.Managers;

namespace Demo.Behaviours.Player
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject BulletPrefab;

        private Vector2 _playerMovement;
        private float _fireRate = 0.5f;
        public float FireRate
        {
            set
            {
                _fireRate = value;
            }
        }
        private float _nextFire = 0.0f;
        private bool _canShoot = false;
        public bool AnimationEnded = false;

        void Update()
        {
            if (_canShoot && Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }
        public void UpdateMovementValue(Vector2 playerMovement)
        {
            _playerMovement = playerMovement;
            // BulletPrefabBehaviour.PlayerBulletSpeed = playerMovement.x;
        }

        public void CanShoot()
        {
            _canShoot=true;
        }

        public void CannotShoot()
        {
            _canShoot=false;
        }

        private void Shoot()
        {
            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            CameraShakeBehaviour.Instance.ShakeCamera(0.5f,0.2f);
            SoundManager.Instance.Play("Shoot");
        }
    }
}
