using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours
{
    public class CollisionBehaviour : MonoBehaviour
    {
        private IDamageable _damageable;
        private int _damage = 12;
        private void OnCollisionEnter2D(Collision2D other)
        {
            _damageable = other.gameObject.GetComponent<IDamageable>();
            if(_damageable != null)
            {
                _damageable.Damage(_damage);
            }
        }
        private void OnCollisionStay2D(Collision2D other)
        {
            _damageable = other.gameObject.GetComponent<IDamageable>();
            if(_damageable != null)
            {
                _damageable.Damage(_damage);
            }
        }
    }
}
