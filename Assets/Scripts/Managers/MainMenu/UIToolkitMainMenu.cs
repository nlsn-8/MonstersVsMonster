using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Managers
{
    public class UIToolkitMainMenu : MonoBehaviour
    {
        public MainMenuUIManager UI;
        public UIDocument document;
        private Button _playButton;
        private Button _settingsButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _playButton = root.Query<Button>("PlayButton");
            _settingsButton = root.Q<Button>("SettingsButton");
            _playButton.RegisterCallback<ClickEvent>(StartGame);
            _settingsButton.RegisterCallback<ClickEvent>(OpenSettingsPanel);
        }

        private void OpenSettingsPanel(ClickEvent e)
        {
            UI.OnClickMainMenuSettings();
        }
        
        private void StartGame(ClickEvent e)
        {
            UI.OnClickStartGame();
        }
    }
}
