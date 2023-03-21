using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Demo.Managers;

namespace Demo.UIToolkit
{
    public class UIToolkitPopup : MonoBehaviour
    {
        public GameManager GM;
        public UIDocument document;
        private Button _closeButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _closeButton = root.Query<Button>("CloseButton");
            _closeButton.RegisterCallback<ClickEvent>(ClosePopup);
            
            GM.PauseGame();
        }
        
        private void ClosePopup(ClickEvent e)
        {
            GM.TogglePauseState(true);
        }
    }
}
