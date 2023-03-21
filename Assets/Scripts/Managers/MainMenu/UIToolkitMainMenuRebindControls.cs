using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Managers
{
    public class UIToolkitMainMenuRebindControls : MonoBehaviour
    {
        public MainMenuUIManager UI;
        public UIDocument document;
        private Button _BackButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _BackButton = root.Q<Button>("BackButton");
            _BackButton.RegisterCallback<ClickEvent>(GoBackMainMenuSettings);
        }
        
        private void GoBackMainMenuSettings(ClickEvent e)
        {
            UI.OnClickGoBackMainMenuSettings();
        }
    }
}
