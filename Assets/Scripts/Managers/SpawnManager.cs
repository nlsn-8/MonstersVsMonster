using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

namespace Demo.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject[] EnemyPrefabs;
        public Transform[] SpawnPoints;

        public SoundManager soundManager;

        private Random _random;
        private int _randomPosition;
        private int _randomEnemy;

        // Start is called before the first frame update
        void Start()
        {
            _random = new Random();
            InvokeRepeating("SpawnEnemies", 2f, 9f);
        }

        private void SpawnEnemies()
        {
            _randomPosition = _random.Next(0, SpawnPoints.Length);
            _randomEnemy = _random.Next(0, EnemyPrefabs.Length);
            Instantiate(EnemyPrefabs[_randomEnemy], SpawnPoints[_randomPosition].position, Quaternion.identity);
            soundManager.Play("Spawn");
        }
    }
}
