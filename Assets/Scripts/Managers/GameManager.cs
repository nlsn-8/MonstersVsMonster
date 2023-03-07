using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public UIManager UI;
        public static Action GameHasStarted;
        public static Action GameHasFinished;

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

        public static void GameStarted()
        {
            if(GameHasStarted != null)
            {
                GameHasStarted();
            }
        }

        public static void GameFinished()
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
        }

        public void AddToCount(int i)
        {
            _enemyKills += i;
            UI.UpdateKillCountText(_enemyKills);
        }
        
    }
}
