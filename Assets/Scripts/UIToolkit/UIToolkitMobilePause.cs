using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Demo.Managers;

namespace Demo.UIToolkit
{
    public class UIToolkitMobilePause : MonoBehaviour
    {
        public GameManager GM;
        public UIDocument document;
        private Button _closeButton;

        private void OnEnable()
        {
            VisualElement root = document.rootVisualElement;

            _closeButton = root.Query<Button>("CloseButton");
            _closeButton.RegisterCallback<ClickEvent>(UnpauseGame);
        }
        
        private void UnpauseGame(ClickEvent e)
        {
            GM.MobilePauseState();
        }
    }
}