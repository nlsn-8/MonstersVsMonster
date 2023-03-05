using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public Sound[] Sounds;
        private static Dictionary<string, float> _soundTimerDictionary;

        private static SoundManager _instance;
        public static SoundManager Instance
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

            if (!CanPlaySound(sound)) return;
            
            // to make it editable in runplay
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.Play();
        }

        public void Stop(string name)
        {
            Sound sound = Array.Find(Sounds, s => s.name == name);

            if (sound == null)
            {
                Debug.LogError("Sound " + name + " Not Found!");
                return;
            }
            
            sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volume / 2f, sound.volume / 2f));
            sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitch / 2f, sound.pitch / 2f));
            
            sound.source.Stop();
        }

        public void PlayOneShoot()
        {
            Sound sound = Array.Find(Sounds, s => s.name == name);
            sound.source.PlayOneShot(sound.clip);
        }

        private static bool CanPlaySound(Sound sound)
        {
            if (_soundTimerDictionary.ContainsKey(sound.name))
            {
                float lastTimePlayed = _soundTimerDictionary[sound.name];

                if (lastTimePlayed + sound.clip.length < Time.time)
                {
                    _soundTimerDictionary[sound.name] = Time.time;
                    return true;
                }
                return false;
            }
            return true;
        }

        public AudioSource GetSource(string name)
        {
            Sound sound = Array.Find(Sounds, s => s.name == name);

            if (sound == null)
            {
                Debug.LogError("Sound " + name + " Not Found!");
                return null;
            }
            
            return sound.source;
        }
    }
}
