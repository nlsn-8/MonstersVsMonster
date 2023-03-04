using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Behaviours.Weapon;

namespace Demo.Behaviours.Player
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject BulletPrefab;

        private Vector2 _playerMovement;
        private float _fireRate = 0.5f;
        private float _nextFire = 0.0f;
        private bool _canShoot = false;
        public bool AnimationEnded = false;

        void Update()
        {
            if (_canShoot && Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            }
        }
        public void UpdateMovementValue(Vector2 playerMovement)
        {
            _playerMovement = playerMovement;
            // BulletPrefabBehaviour.PlayerBulletSpeed = playerMovement.x;
        }

        public void Shoot()
        {
            // if(Mathf.Approximately(_playerMovement.sqrMagnitude,0) && AnimationEnded)
            // {
            //     _canShoot = true;
            // }
            // else if(_playerMovement.x != 0)
            // {
            //     _canShoot = true;
            // }
            _canShoot=true;
        }

        public void CannotShoot()
        {
            _canShoot=false;
        }
    }
}
