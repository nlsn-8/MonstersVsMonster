using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Demo.Managers
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text KillCountText;
        public SoundManager soundManager;
        public GameObject GameOverPanel;
        public Slider slider;
        public TMP_Text ProgressText;

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
        // Invoked from Start button first scene
        public void OnClickStartGame()
        {
            soundManager.Play("Start");
            LoadLevel(1);
        }
        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }

        private IEnumerator LoadAsynchronously (int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                ProgressText.text = progress * 100f + "%";
                yield return null;
            }
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
