using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Demo.Behaviours.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerMovementBehaviour))]

    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementBehaviour PlayerMovement;
        public PlayerAnimationBehaviour PlayerAnimation;
        private Vector2 _movement;

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                PlayerMovement.Jump();
                PlayerAnimation.PlayJumpAnimation();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                PlayerAnimation.PlayShootAnimation();
            }
        }

        public void OnDuck(InputAction.CallbackContext context)
        {
            if(context.started || context.performed)
            {
                if(Mathf.Approximately(_movement.sqrMagnitude, 0))
                {
                    PlayerMovement.Duck();
                    PlayerAnimation.PlayDuckAnimation();
                }
            }
            else if(context.canceled)
            {
                PlayerMovement.Stand();
                PlayerAnimation.PlayStandAnimation();
            }
        }

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                PlayerAnimation.PlayStandAnimation();
            }
        }
    }
}
