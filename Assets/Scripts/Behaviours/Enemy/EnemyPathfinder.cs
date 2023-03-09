using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

using Demo.Behaviours;
using Demo.Managers;

namespace Demo.Behaviours.Enemy
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyPathfinder : PathfinderBehaviour
    {
        // Start is called before the first frame update
        public override void Start()
        {
            _targetObject = SpawnManager.GetGameObject();
            if(_targetObject == null ) return;
            _targetToFollow = _targetObject.GetComponent<Transform>();
            base.Start();
        }
    }
}
