using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours.Player
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        public Animator PlayerAnimator;
        public SpriteRenderer PlayerSprite;

        private Vector2 _playerMovement;
        private int _playerHorizontalID;
        private int _playerShootID;
        private int _playerStandID;
        private int _playerDuckID;
        private int _playerHurtID;
        private int _playerJumpID;
        private bool _flipSprite;
        private bool _isDucking = false;

        private float _currentTime;
        private int _timeToAction = 5;

        // Start is called before the first frame update
        void Start()
        {
            _playerHorizontalID = Animator.StringToHash("Horizontal");
            _playerShootID = Animator.StringToHash("Shoot");
            _playerStandID = Animator.StringToHash("Stand");
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
        }

        public void PlayJumpAnimation()
        {
            PlayerAnimator.SetBool(_playerJumpID, true);
        }

        public void PlayDuckAnimation()
        {
            PlayerAnimator.SetBool(_playerDuckID, true);
            _isDucking = true;
        }

        public void PlayStandAnimation()
        {
            PlayerAnimator.SetBool(_playerJumpID, false);
            PlayerAnimator.SetBool(_playerDuckID, false);
            PlayerAnimator.SetTrigger(_playerStandID);
            _isDucking = false;
        }

        public void PlayShootAnimation()
        {
            if(Mathf.Approximately(_playerMovement.sqrMagnitude, 0))
            {
                PlayerAnimator.SetTrigger(_playerShootID);
                if(!_isDucking) ResetTime();
            }
        }

        private void ResetTime()
        {
            _currentTime = 0f;
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
