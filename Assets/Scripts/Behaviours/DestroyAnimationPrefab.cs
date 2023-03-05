using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours
{
    public class DestroyAnimationPrefab : MonoBehaviour
    {
        // invoked using Animation events
        public void DestroyAnimationGameObject()
        {
            Destroy(this.gameObject);
        }
    }
}
