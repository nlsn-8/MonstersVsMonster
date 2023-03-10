using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours.Player
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        public Rigidbody2D PlayerRigidbody;
        public BoxCollider2D PlayerBoxCollider;
        public CircleCollider2D PlayerCircleCollider;

        private bool _isGrounded;
        public bool IsGrounded
        {
            get
            {
                return _isGrounded;
            }
        }
        private Vector2 _playerMovement;
        private Vector2 _playerRigidbodyMovement;
        public Vector2 PlayerMovementVector
        {
            get
            {
                return _playerRigidbodyMovement;
            }
        }
        
        private int _playerVelocity = 9;
        private int _playerJumpForce = 30;

        private void FixedUpdate()
        {
            Move();
            _isGrounded = GroundChecker();
        }

        public void UpdateMovementValue(Vector2 move)
        {
            _playerMovement = move * _playerVelocity;// * Time.fixedDeltaTime;
        }

        private void Move()
        {
            PlayerRigidbody.velocity = new Vector2(_playerMovement.x, PlayerRigidbody.velocity.y);
            _playerRigidbodyMovement = PlayerRigidbody.velocity;
        }

        public void Duck()
        {
            PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            PlayerBoxCollider.enabled = false;
        }
        public void Stand()
        {
            PlayerRigidbody.constraints = RigidbodyConstraints2D.None; 
            PlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation; 
            PlayerBoxCollider.enabled = true;
        }

        private bool GroundChecker()
        {
            if(PlayerCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Jump()
        {
            if(_isGrounded)
            {
                PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, _playerJumpForce);
            }
        }
    }
}