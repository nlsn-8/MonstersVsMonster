using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Demo.Behaviours
{
    public class CameraShakeBehaviour : MonoBehaviour
    {
        private CinemachineVirtualCamera _cinemachineCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineCameraPerlin;
        private float _shakerTime;
        private float _totalTime;
        private float _intensity;
        private bool _isSuddenShake;

        private static CameraShakeBehaviour _instance;
        public static CameraShakeBehaviour Instance
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

        // Start is called before the first frame update
        void Start()
        {
            _cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
            if (_cinemachineCamera == null) return;
            _cinemachineCameraPerlin = _cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float intensity, float time, bool suddenShake = true)
        {
            if(_cinemachineCameraPerlin != null )
            {
                _cinemachineCameraPerlin.m_AmplitudeGain = intensity;
                _shakerTime = time;
                _totalTime = time;
                _intensity = intensity;
                _isSuddenShake = suddenShake;
            }
        }

        private void Update()
        {
            if(_shakerTime > 0)
            {
                _shakerTime -= Time.deltaTime;
                if(_isSuddenShake && _shakerTime <= 0)
                {
                    _cinemachineCameraPerlin.m_AmplitudeGain = 0;
                }
                else if(!_isSuddenShake)
                {
                    _cinemachineCameraPerlin.m_AmplitudeGain = Mathf.Lerp(_intensity,0, _shakerTime/_totalTime);
                }
            }
        }
    }
}
