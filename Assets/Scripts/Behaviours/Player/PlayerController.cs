using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Demo.Managers;

namespace Demo.Behaviours.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerMovementBehaviour))]
    [RequireComponent(typeof(PlayerInput))]

    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementBehaviour PlayerMovement;
        public PlayerAnimationBehaviour PlayerAnimation;
        public PlayerWeaponBehaviour PlayerWeapon;
        public PlayerInput PlayerInputs;
        private Vector2 _movement;
        private bool _activateWeapon;

        private string _actionMapPlayerControls = "Player";
        private string _actionMapUIControls = "UI";

        private void OnEnable()
        {
            GameManager.Instance.SwitchPlayerInputMap += UpdateInputState;
        }

        private void OnDisable()
        {
            GameManager.Instance.SwitchPlayerInputMap -= UpdateInputState;
        }

        #region InputEvents
        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>().normalized;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started || context.performed)
            {
                PlayerMovement.Jump();
                PlayerAnimation.PlayJumpAnimation();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if(context.started || context.performed)
            {
                _activateWeapon = true;
                PlayerAnimation.PlayShootAnimation(_activateWeapon);
                PlayerWeapon.CanShoot();
            }
            else if(context.canceled)
            {
                PlayerWeapon.CannotShoot();
            }
        }

        public void OnHideWeapon(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                _activateWeapon = false;
                PlayerAnimation.UpdateWeaponState(_activateWeapon);
            }
        }

        public void OnDuck(InputAction.CallbackContext context)
        {
            if(!PlayerMovement.IsGrounded)
                return;
            if(context.started || context.performed)
            {
                PlayerMovement.Duck();
                PlayerAnimation.PlayDuckAnimation();
            }
            else if(context.canceled)
            {
                PlayerMovement.Stand();
                PlayerAnimation.PlayStandAnimation();
            }
        }
        
        public void OnPause(InputAction.CallbackContext value)
        {
            if(value.started)
            {
                GameManager.Instance.TogglePauseState();
            }
        }
        #endregion

        #region UpdateBehaviours
        private void Update()
        {
            UpdatePlayerMovement();
            UpdatePlayerAnimation();
        }

        private void UpdatePlayerMovement()
        {
            PlayerMovement.UpdateMovementValue(_movement);
        }

        private void UpdatePlayerAnimation()
        {
            PlayerAnimation.UpdateMovementValue(PlayerMovement.PlayerMovementVector);
        }
        #endregion

        #region GameIsPaused
        private void UpdateInputState(bool isPaused)
        {
            switch (isPaused)
            {
                case true:
                    PlayerInputs.DeactivateInput();
                    EnableUIControls();
                    break;
                case false:
                    EnableGameplayControls();
                    PlayerInputs.ActivateInput();
                    break;
            }
        }

        private void EnableUIControls()
        {
            PlayerInputs.SwitchCurrentActionMap(_actionMapUIControls);
        }

        private void EnableGameplayControls()
        {
            PlayerInputs.SwitchCurrentActionMap(_actionMapPlayerControls);  
        }
        #endregion

        #region DetectPlayerCollisions
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                PlayerAnimation.IsGrounded = true;
                PlayerAnimation.StopJumpAnimation();
            }
        }
        private void OnCollisionStay2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                PlayerAnimation.IsGrounded = true;
            }
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                PlayerAnimation.IsGrounded = false;
            }
        }
        #endregion
    }
}
