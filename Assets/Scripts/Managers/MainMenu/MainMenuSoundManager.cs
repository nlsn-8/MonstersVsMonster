using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class MainMenuSoundManager : MonoBehaviour
    {
        public Sound[] Sounds;
        private static Dictionary<string, float> _soundTimerDictionary;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _soundTimerDictionary = new Dictionary<string, float>();

            foreach (Sound sound in Sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.playOnAwake = false;

                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.isLoop;
                sound.source.playOnAwake = sound.playOnAwake;

                if (sound.hasCooldown)
                {
                    Debug.Log(sound.name);
                    _soundTimerDictionary[sound.name] = 0f;
                }
            }
        }

        private void Start()
        {
            // Add this part after having a theme song
            Play("Music");
        }

        public void Play(string name)
        {
            Sound sound = Array.Find(Sounds, s => s.name == name);

            if (sound == null)
            {
                Debug.LogError("Sound " + name + " Not Found!");
                return;
            }

            // to make it editable in runplay
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.Play();
        }
    }
}
