using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours
{
    public interface IDamageable
    {
        int Health {get;set;}
        int Resistance {get;set;}
        void Damage();
    }
}
