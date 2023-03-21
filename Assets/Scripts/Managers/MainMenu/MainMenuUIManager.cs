using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Demo.Managers
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public MainMenuSoundManager SoundManager;
        [Header("Main Scene")]
        public TMP_Text ProgressText;
        public Slider ProgressSlider;
        public GameObject SettingsPanel;
        public GameObject RebindingPanel;

        // Invoked from play button first scene
        public void OnClickStartGame()
        {
            SoundManager.Play("Start");
            // SceneManager.LoadScene("1");
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

        public void SettingsMenu(bool boolean)
        {
            SettingsPanel.SetActive(boolean);
        }

        public void RebindMenu(bool boolean)
        {
            RebindingPanel.SetActive(boolean);
        }
        
        public void OnClickMainMenuSettings()
        {
            SettingsMenu(true);
        }
        public void OnClickGoBackMainMenuSettings()
        {
            SettingsMenu(true);
            RebindMenu(false);
        }
        public void OnClickMainMenuControls()
        {
            SettingsMenu(false);
            RebindMenu(true);
        }
        public void OnClickGoBackMainMenu()
        {
            SettingsMenu(false);
        }
    }
}
