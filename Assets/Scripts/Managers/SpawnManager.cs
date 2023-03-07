using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

namespace Demo.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameObject PlayerPrefab;
        public Transform PlayerSpawnPoint;
        public GameObject[] EnemyPrefabs;
        public Transform[] SpawnPoints;

        private static GameObject _Player;
        private Random _random;
        private int _randomPosition;
        private int _randomEnemy;
        private bool _hasGameFinished = false;
        private GameObject EnemyInstantiated;
        private List<GameObject> EnemiesInstantiated;

        private void OnEnable()
        {
            GameManager.GameHasFinished += GameFinished;
            GameManager.GameHasStarted += GameStarted;
        }

        private void OnDisable()
        {
            GameManager.GameHasFinished -= GameFinished;
            GameManager.GameHasStarted -= GameStarted;
        }

        private void Awake()
        {
            _random = new Random();
            EnemiesInstantiated = new();
            _Player = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);
        }

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("SpawnEnemies", 2f, 9f);
        }

        private void SpawnEnemies()
        {
            if(_hasGameFinished) return;
            _randomPosition = _random.Next(0, SpawnPoints.Length);
            _randomEnemy = _random.Next(0, EnemyPrefabs.Length);
            EnemyInstantiated = Instantiate(EnemyPrefabs[_randomEnemy], SpawnPoints[_randomPosition].position, Quaternion.identity);
            EnemiesInstantiated.Add(EnemyInstantiated);
            soundManager.Play("Spawn");
        }

        private void GameFinished()
        {
            _hasGameFinished = true;
        }
        private void GameStarted()
        {
            _hasGameFinished = false;
            _Player = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);
            foreach (var go in EnemiesInstantiated)
            {
                Destroy(go);
            }
        }

        public static GameObject GetGameObject()
        {
            return _Player;
        }
    }
}
