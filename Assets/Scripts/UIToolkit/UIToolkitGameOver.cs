using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Demo.Managers;

namespace Demo.UIToolkit
{
    public class UIToolkitGameOver : MonoBehaviour
    {
        public GameManager GM;
        public UIDocument document;
        private Button _restartButton;
        private Label _gameOverText; 
        public float CallbackTime = 1.1f;
        private float _time = 0f;
        public float TimeBetweenClass = 0.6f;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _gameOverText = root.Q<Label>("GameOverText");
            _restartButton = root.Q<Button>("RestartButton");
            _restartButton.RegisterCallback<ClickEvent>(RestartGame);
        }

        private void RestartGame(ClickEvent e)
        {
            GM.OnClickStartGame();
        }

        private void Update()
        {
            if(Time.time > _time)
            {
                _time = Time.time + CallbackTime;
                StartRoutine();
            }
        }

        private void StartRoutine()
        {
            StartCoroutine(ChangeClass());
        }

        private IEnumerator ChangeClass()
        {
            _gameOverText.AddToClassList("gameOverText2");
            _restartButton.AddToClassList("gameOverButton2");
            yield return new WaitForSeconds(TimeBetweenClass);
            _gameOverText.RemoveFromClassList("gameOverText2");
            _restartButton.RemoveFromClassList("gameOverButton2");
        }
    }
}