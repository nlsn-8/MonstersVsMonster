using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Demo.Managers
{
    public class MobileDetector : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern bool _isMobile();

        [Header("[Player Mobile UI]")]
        public GameObject[] ScreenButtons;

        public static Action MobileDevice;

        void OnEnable()
        {
            if (IsMobile())
            {
                foreach (var item in ScreenButtons)
                {
                    item.SetActive(true);
                }

                if(MobileDevice != null)
                {
                    MobileDevice();
                }
            }
            else
            {
                foreach (var item in ScreenButtons)
                {
                    item.SetActive(false);
                }
            }
        }

        public bool IsMobile()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return _isMobile();
#endif
            return false;
        }
    }
}
