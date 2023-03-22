using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Demo.Managers;

namespace Demo.UIToolkit
{
    public class UIToolkitRebindControls : MonoBehaviour
    {
        public UIManager UI;
        public UIDocument document;
        private Button _backButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _backButton = root.Query<Button>("BackButton");
            _backButton.RegisterCallback<ClickEvent>(GoBackToSettings);
        }

        private void GoBackToSettings(ClickEvent e)
        {
            UI.OnClickGoBackToSettings();
        }
    }
}
