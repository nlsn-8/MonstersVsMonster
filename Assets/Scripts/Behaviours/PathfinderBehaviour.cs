using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Demo.Behaviours.Enemy
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PathfinderBehaviour : MonoBehaviour
    {
        protected GameObject _targetObject;
        protected Transform _targetToFollow;
        protected Rigidbody2D EnemyRigidbody;
        protected Seeker EnemySeeker;

        protected Path _path;
        // _nextWaypointDistance means how close this GO needs
        // to be to a waypoint before it moves to the next one
        protected float _nextWaypointDistance = 3f;
        protected int _currentWaypoint = 0;
        protected bool _reachedEndOfPath = false;
        protected float _speed = 200f;
        protected Vector2 _force;
        protected bool _flipTransform;

        public virtual void Awake()
        {
            EnemyRigidbody = GetComponent<Rigidbody2D>();
            EnemySeeker = GetComponent<Seeker>();
        }
        // Start is called before the first frame update
        public virtual void Start()
        {
            // method's name, amount of time to wait until the callback method, repeat rate
            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        public virtual void UpdatePath()
        {
            if(_targetToFollow == null ) return;
            // make sure we aren't making a new path if previous one hasn't finished
            if(EnemySeeker.IsDone())
            {
                // makes path and after calculating the path OnPathComplete gets called
                EnemySeeker.StartPath(EnemyRigidbody.position, _targetToFollow.position, OnPathComplete);
            }
        }

        public virtual void OnPathComplete(Path p)
        {
            if(!p.error) // we didn't get any errors
            {
                _path = p;
                _currentWaypoint = 0;
            }
        }

        public virtual void FixedUpdate()
        {
            MoveEnemyThroughPath();
            FlipEnemyTransform();
        }

        public virtual void MoveEnemyThroughPath()
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

        public virtual void FlipEnemyTransform()
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
