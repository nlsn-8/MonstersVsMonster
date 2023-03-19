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
        public GameManager GameManager;
        public SoundManager soundManager;
        [Header("Main Scene")]
        public TMP_Text ProgressText;
        public Slider ProgressSlider;
        [Header("Game Scene")]
        public TMP_Text KillCountText;
        public GameObject GameOverPanel;
        public GameObject PauseMenuPanel;
        public GameObject SettingsPanel;
        public GameObject RebindingPanel;
        public GameObject PopupPanel;

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

        private void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }

        private IEnumerator LoadAsynchronously (int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                ProgressSlider.value = progress;
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

        public void PauseMenu(bool isPaused)
        {
            PauseMenuPanel.SetActive(isPaused);
            SettingsMenu(false);
            RebindMenu(false);
            ClosePopupMenu();
        }
        public void ClosePauseMenu()
        {
            PauseMenuPanel.SetActive(false);
        }
        public void OpenPauseMenu()
        {
            PauseMenuPanel.SetActive(true);
        }

        public void SettingsMenu(bool boolean)
        {
            SettingsPanel.SetActive(boolean);
        }

        public void RebindMenu(bool boolean)
        {
            RebindingPanel.SetActive(boolean);
        }

        public void ClosePopupMenu()
        {
            PopupPanel.SetActive(false);
        }

        public void OnClickGoBackToSettings()
        {
            RebindMenu(false);
            SettingsMenu(true);
        }
        public void OnClickGoBackToPause()
        {
            SettingsMenu(false);
            OpenPauseMenu();
        }

        public void OnClickSettings()
        {
            SettingsMenu(true);
            ClosePauseMenu();
        }
        public void OnClickControls()
        {
            RebindMenu(true);
            SettingsMenu(false);
        }
    }
}
