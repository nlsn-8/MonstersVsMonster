using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo.Managers
{
    public class UIManager : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameObject GameOverPanel;

        private void OnEnable()
        {
            GameManager.GameHasFinished += GameFinished;
            GameManager.GameHasStarted += GameStarted;
        }

        private void OnDisable()
        {
            GameManager.GameHasFinished -= GameFinished;
            GameManager.GameHasStarted -= GameStarted;
        }

        public void OnClickStartGame()
        {
            soundManager.Play("Start");
            SceneManager.LoadScene("2");
        }

        private void GameFinished()
        {
            GameOverPanel.SetActive(true);
        }
        private void GameStarted()
        {
            GameOverPanel.SetActive(false);
        }
    }
}
