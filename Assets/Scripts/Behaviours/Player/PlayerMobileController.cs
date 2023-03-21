using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours.Player
{
    public class PlayerMobileController : MonoBehaviour
    {
        private PlayerController _playerController;
        public PlayerController PlayerControllerReference
        {
            get
            {
                return _playerController;
            }
        }
        private void OnEnable()
        {
            PlayerController.PlayerControllerInstance += GetPlayerControllerReference;
        }

        private void OnDisable()
        {
            PlayerController.PlayerControllerInstance -= GetPlayerControllerReference;
        }

        private void GetPlayerControllerReference(PlayerController playerController)
        {
            _playerController = playerController;
        }
    }
}
