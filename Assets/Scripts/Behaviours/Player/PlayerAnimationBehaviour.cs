using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Demo.Managers;

namespace Demo.Behaviours.Player
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {

        public Animator PlayerAnimator;
        public PlayerWeaponBehaviour PlayerWeapon;

        private Vector2 _playerMovement;
        private int _playerHorizontalID;
        private int _playerShootID;
        private int _playerDuckID;
        private int _playerHurtID;
        private int _playerJumpID;
        private bool _flipSprite;
        private bool _isDucking = false;
        private bool _isShooting = false;
        public bool IsGrounded;

        // Start is called before the first frame update
        void Start()
        {
            _playerHorizontalID = Animator.StringToHash("Horizontal");
            _playerShootID = Animator.StringToHash("Shoot");
            _playerDuckID = Animator.StringToHash("Duck");
            _playerHurtID = Animator.StringToHash("Hurt");
            _playerJumpID = Animator.StringToHash("Jump");

        }

        private void Update()
        {
            UpdateMovementAnimation(_playerMovement.x);
        }

        public void UpdateMovementValue(Vector2 movementVector)
        {
            _playerMovement = movementVector;
            FlipSprite(movementVector.x);
        }

        public void UpdateMovementAnimation(float movementBlendValue)
        {
            float absolute = Mathf.Abs(movementBlendValue);
            PlayerAnimator.SetFloat(_playerHorizontalID, absolute);
            // if(absolute > 0) SoundManager.Instance.Play("Walk");
        }

        public void PlayJumpAnimation()
        {
            if(IsGrounded)
            {
                PlayerAnimator.SetBool(_playerJumpID, IsGrounded);
                SoundManager.Instance.Play("Jump");
            }
        }
        public void StopJumpAnimation()
        {
            PlayerAnimator.SetBool(_playerJumpID, false);
            SoundManager.Instance.Play("Land");
        }

        public void PlayDuckAnimation()
        {
            PlayerAnimator.SetBool(_playerDuckID, true);
            _isDucking = true;
        }

        public void PlayStandAnimation()
        {
            PlayerAnimator.SetBool(_playerDuckID, false);
            _isDucking = false;
        }

        public void PlayShootAnimation(bool weaponState)
        {
            PlayerAnimator.SetBool(_playerShootID, weaponState);
        }

        public void UpdateWeaponState(bool weaponState)
        {
            _isShooting = weaponState;
            PlayerAnimator.SetBool(_playerShootID,_isShooting);
        }
        
        // Invoked from shoot_animation Animation Event
        private void PlayerWeaponAnimationEnded()
        {
            PlayerWeapon.AnimationEnded = true;
        }
        // Invoked from shoot_animation Animation Event
        private void PlayerWeaponAnimationStarted()
        {
            PlayerWeapon.AnimationEnded = false;
        }

        private void FlipSprite(float value)
        {
            if(value < 0 && !_flipSprite)
            {
                _flipSprite = !_flipSprite;
                transform.Rotate(0,180,0);
                // PlayerSprite.flipX = true;
            }
            else if(value > 0 && _flipSprite)
            {
                _flipSprite = !_flipSprite;
                transform.Rotate(0,180,0);
                // PlayerSprite.flipX = false;
            }
        }
    }
}
