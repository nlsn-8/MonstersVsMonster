using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public UIManager UI;
        public Action GameHasStarted;
        public Action GameHasFinished;
        public Action<bool> SwitchPlayerInputMap;

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
            _instance = this;
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

        public void TogglePauseState()
        {
            PauseGame();
            UpdateUIMenu();
        }

        public void PauseGame()
        {
            _isPaused = !_isPaused;
            ChangeTimeScale();
            ChangePlayerInputMap();
        }

        private void UpdateUIMenu()
        {
            UI.UpdateUIMenu(_isPaused);
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
