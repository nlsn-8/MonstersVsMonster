using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours.Player
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {

        //TODO:
        //fix firepoint location when ducking
        //shoot when ducking animation has finished
        //shoot faster when ducking
        //If standing and shoot, play complete animation
        //back to unarmed idle after 5f (optional)

        public Animator PlayerAnimator;
        public SpriteRenderer PlayerSprite;
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

        private float _currentTime;
        private int _timeToAction = 5;

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
            
            // _currentTime += Time.deltaTime;
            // if(_currentTime >= _timeToAction)
            // {
            //     PlayStandAnimation();
            // }
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
        }

        public void PlayJumpAnimation()
        {
            if(IsGrounded) PlayerAnimator.SetBool(_playerJumpID, IsGrounded);
        }
        public void StopJumpAnimation()
        {
            PlayerAnimator.SetBool(_playerJumpID, false);
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
            if(!_isDucking) ResetTime();
        }

        public void UpdateWeaponState(bool weaponState)
        {
            _isShooting = weaponState;
            PlayerAnimator.SetBool(_playerShootID,_isShooting);
        }

        private void ResetTime()
        {
            _currentTime = 0f;
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
