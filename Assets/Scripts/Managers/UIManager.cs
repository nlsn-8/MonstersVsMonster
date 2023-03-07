using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Demo.Managers
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text KillCountText;
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

        public void UpdateKillCountText(int i)
        {
            KillCountText.text = i.ToString();
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
