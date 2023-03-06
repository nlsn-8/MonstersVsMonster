using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo.Managers
{
    public class UIManager : MonoBehaviour
    {
        public SoundManager soundManager;

        public void OnClickStartGame()
        {
            soundManager.Play("Start");
            SceneManager.LoadScene("2");
        }
    }
}
