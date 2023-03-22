using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Demo.Behaviours.Player
{
    public class PlayerReference : MonoBehaviour
    {
        private CinemachineVirtualCamera _followCam;

        // Start is called before the first frame update
        void Start()
        {
            _followCam = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
            if(_followCam != null )
            {
                _followCam.Follow = this.gameObject.transform;
            }
        }
    }
}
