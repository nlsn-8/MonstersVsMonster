using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Managers
{
    public class UIToolkitPause : MonoBehaviour
    {
        public UIManager UI;
        public GameManager GM;
        public UIDocument document;
        private Button _settingsButton;
        private Button _closeButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _settingsButton = root.Q<Button>("SettingsButton");
            _closeButton = root.Query<Button>("CloseButton");
            _settingsButton.RegisterCallback<ClickEvent>(OpenSettingsPanel);
            _closeButton.RegisterCallback<ClickEvent>(UnpauseGame);
        }

        private void OpenSettingsPanel(ClickEvent e)
        {
            UI.OnClickSettings();
        }
        
        private void UnpauseGame(ClickEvent e)
        {
            GM.TogglePauseState();
        }
    }
}