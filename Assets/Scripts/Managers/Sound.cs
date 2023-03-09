using UnityEngine;

namespace Demo.Managers
{
    [System.Serializable]
    public class Sound
    {
        public string  name;
        [HideInInspector]
        public AudioSource source;
        public AudioClip clip;

        [Range(0f, 1f) ]
        public float volume = 1f;
        [Range(.1f, 3f)]
        public float pitch = 1f;
        public bool isLoop;
        public bool playOnAwake;
        public bool hasCooldown;
    }
}
