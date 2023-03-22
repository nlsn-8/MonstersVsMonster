using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public UIManager UI;
        public event Action GameHasStarted;
        public event Action GameHasFinished;
        public event Action<bool> SwitchPlayerInputMap;

        private bool _isPaused;
        public bool IsPaused
        {
            get
            {
                return _isPaused;
            }
        }
        private int _enemyKills;
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                _instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void GameStarted()
        {
            if(GameHasStarted != null)
            {
                GameHasStarted();
            }
        }

        public void GameFinished()
        {
            if(GameHasFinished != null)
            {
                GameHasFinished();
            }
        }

        // Invoked from restart button second scene
        public void OnClickStartGame()
        {
            GameStarted();
            _enemyKills = 0;
            AddToCount(_enemyKills);
        }

        public void AddToCount(int i)
        {
            _enemyKills += i;
            UI.UpdateKillCountText(_enemyKills);
        }
        
        #region GameIsPaused

        public void TogglePauseState(bool popupMenu = false)
        {
            PauseGame();
            if(!popupMenu)
            {
                UpdateUIMenu();
            }
            else if(popupMenu)
            {
                ClosePopupMenu();
            }
        }

        public void MobilePauseState()
        {
            PauseGame();
            UI.MobilePauseGame(_isPaused);
        }
        public void PauseGame()
        {
            _isPaused = !_isPaused;
            ChangeTimeScale();
            ChangePlayerInputMap();
        }

        private void ClosePopupMenu()
        {
            UI.ClosePopupMenu();
        }

        private void UpdateUIMenu()
        {
            UI.PauseMenu(_isPaused);
        }

        private void ChangeTimeScale()
        {
            float newTimeScale = 0f;

            switch(_isPaused)
            {
                case true:
                    newTimeScale = 0f;
                    break;
                case false:
                    newTimeScale = 1f;
                    break;
            }
            Time.timeScale = newTimeScale;
        }

        private void ChangePlayerInputMap()
        {
            if(SwitchPlayerInputMap != null) SwitchPlayerInputMap(_isPaused);
        }
        #endregion
    }
}
