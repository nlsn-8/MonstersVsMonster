using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Demo.Behaviours.Enemy
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyPathfinder : MonoBehaviour
    {
        private GameObject _playerObject;
        private Transform _playerTarget;
        public Rigidbody2D EnemyRigidbody;
        public Seeker EnemySeeker;

        private Path _path;
        // _nextWaypointDistance means how close this GO needs
        // to be to a waypoint before it moves to the next one
        private float _nextWaypointDistance = 3f;
        private int _currentWaypoint = 0;
        private bool _reachedEndOfPath = false;
        private float _speed = 200f;
        private Vector2 _force;
        private bool _flipTransform;

        // Start is called before the first frame update
        void Start()
        {
            _playerObject = GameObject.Find("Player");
            _playerTarget = _playerObject.GetComponent<Transform>();

            // method's name, amount of time to wait until the callback method, repeat rate
            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        private void UpdatePath()
        {
            if(_playerTarget == null ) return;
            // make sure we aren't making a new path if previous one hasn't finished
            if(EnemySeeker.IsDone())
            {
                // makes path and after calculating the path OnPathComplete gets called
                EnemySeeker.StartPath(EnemyRigidbody.position, _playerTarget.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if(!p.error) // we didn't get any errors
            {
                _path = p;
                _currentWaypoint = 0;
            }
        }

        void FixedUpdate()
        {
            MoveEnemyThroughPath();
            FlipEnemyTransform();
        }

        private void MoveEnemyThroughPath()
        {
            if(_path == null)
            {
                return;
            }

            // check if there are more waypoints to keep moving
            if(_currentWaypoint >= _path.vectorPath.Count)
            {
                _reachedEndOfPath = true;
                return;
            }
            else
            {
                _reachedEndOfPath = false;
            }

            // get the direction of the next waypoint's path
            // direction = position of the next waypoint's path minus our position. 
            // Is normalized to get only the direction
            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - EnemyRigidbody.position).normalized;
            _force = direction * _speed * Time.fixedDeltaTime;

            // add force to enemy, set linear drag to stop the enemy moving further in the rb's inspector
            EnemyRigidbody.AddForce(_force);

            // get the distance to the next waypoint
            float distance = Vector2.Distance(EnemyRigidbody.position, _path.vectorPath[_currentWaypoint]);
            if(distance < _nextWaypointDistance)
            {
                _currentWaypoint ++;
            }
        }

        private void FlipEnemyTransform()
        {
            // using force, the enemy turns to the direction it wants to go
            // using rb.velocity.x just turns the enemy to the direction it goes
            if(_force.x < 0 && _flipTransform)
            {
                _flipTransform = !_flipTransform;
                transform.Rotate(0,180,0);
            }
            else if(_force.x > 0 && !_flipTransform)
            {
                _flipTransform = !_flipTransform;
                transform.Rotate(0,180,0);
            }
        }
    }
}
