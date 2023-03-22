using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Demo.Managers;
using Demo.Behaviours.Player;

namespace Demo.UIToolkit
{
    public class UIToolkitMobile : MonoBehaviour
    {
        private PlayerMobileController _playerMobileController;
        public GameManager GM;
        public UIDocument document;
        private Button _jumpButton;
        private Button _pauseButton;
        public static PlayerController playerController;
        private void OnEnable()
        {
            _playerMobileController = GetComponent<PlayerMobileController>();
            VisualElement root = document.rootVisualElement;

            _jumpButton = root.Q<Button>("JumpButton");
            _pauseButton = root.Query<Button>("PauseButton");
            _jumpButton.RegisterCallback<ClickEvent>(PlayerJump);
            _pauseButton.RegisterCallback<ClickEvent>(PauseGame);
        }

        private void PlayerJump(ClickEvent e)
        {
            _playerMobileController.PlayerControllerReference.PlayerMovement.Jump();
        }
        
        private void PauseGame(ClickEvent e)
        {
            GM.MobilePauseState();
        }
    }
}