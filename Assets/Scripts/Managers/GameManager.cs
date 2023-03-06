using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Action GameHasStarted;
        public static Action GameHasFinished;

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
    }
}
