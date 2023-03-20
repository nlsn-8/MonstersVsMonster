using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Managers
{
    public class UIToolkitMainMenuSettings : MonoBehaviour
    {
        public MainMenuUIManager UI;
        public UIDocument document;
        private Button _controlsButton;
        private Button _BackButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _controlsButton = root.Query<Button>("ControlsButton");
            _BackButton = root.Q<Button>("BackButton");
            _controlsButton.RegisterCallback<ClickEvent>(OpenControlsPanel);
            _BackButton.RegisterCallback<ClickEvent>(GoBack);
        }

        private void OpenControlsPanel(ClickEvent e)
        {
            UI.OnClickMainMenuControls();
        }
        
        private void GoBack(ClickEvent e)
        {
            UI.OnClickGoBackMainMenu();
        }
    }
}
