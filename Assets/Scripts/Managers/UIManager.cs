using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Demo.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameManager GameManager;
        [Header("Game Scene")]
        public TMP_Text KillCountText;
        public GameObject GameOverPanel;
        public GameObject PauseMenuPanel;
        public GameObject SettingsPanel;
        public GameObject RebindingPanel;
        public GameObject PopupPanel;
        public GameObject MobilePausePanel;
        public GameObject MobilePanel;

        private void OnEnable()
        {
            GameManager.GameHasFinished += GameFinished;
            GameManager.GameHasStarted += GameStarted;
            MobileDetector.MobileDevice += MobileDevice;
        }

        private void OnDisable()
        {
            GameManager.GameHasFinished -= GameFinished;
            GameManager.GameHasStarted -= GameStarted;
            MobileDetector.MobileDevice -= MobileDevice;
        }

        private void MobileDevice()
        {
            MobilePanel.SetActive(true);
            PopupPanel.SetActive(false);
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

        public void MobilePauseGame(bool pause)
        {
            MobilePausePanel.SetActive(pause);
            MobilePanel.SetActive(!pause);
        }
    }
}
