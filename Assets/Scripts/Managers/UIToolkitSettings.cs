using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Managers
{
    public class UIToolkitSettings : MonoBehaviour
    {
        public UIManager UI;
        public UIDocument document;
        private Button _controlsButton;
        private Button _backButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _controlsButton = root.Q<Button>("ControlsButton");
            _backButton = root.Query<Button>("BackButton");
            _controlsButton.RegisterCallback<ClickEvent>(OpenControlsPanel);
            _backButton.RegisterCallback<ClickEvent>(GoBackToPause);
        }

        private void OpenControlsPanel(ClickEvent e)
        {
            UI.OnClickControls();
        }
        
        private void GoBackToPause(ClickEvent e)
        {
            UI.OnClickGoBackToPause();
        }
    }
}
