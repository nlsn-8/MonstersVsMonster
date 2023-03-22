using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class PopupBehaviour : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.PauseGame();
        }
        
        public void OnClickClosePopup()
        {
            GameManager.Instance.TogglePauseState(true);
        }
    }
}
