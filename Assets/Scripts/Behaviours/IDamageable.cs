using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours
{
    public interface IDamageable
    {
        float Health {get;set;}
        float Resistance {get;set;}
        void Damage(int integer);
    }
}
