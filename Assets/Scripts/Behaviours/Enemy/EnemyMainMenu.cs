using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Demo.Behaviours.Enemy
{
    public class EnemyMainMenu : PathfinderBehaviour
    {
        public Transform[] Waypoints;
        private Random _rand;
        private int _randomNumber;

        public override void Awake()
        {
            base.Awake();
            _rand = new();
            _randomNumber = _rand.Next(0,Waypoints.Length);
        }
        public override void Start()
        {
            _targetToFollow = Waypoints[_randomNumber];
            base.Start();
        }

        void Update()
        {
            if(_reachedEndOfPath)
            {
                _randomNumber = _rand.Next(0,Waypoints.Length);
                _targetToFollow = Waypoints[_randomNumber];
            }
        }
    }
}
