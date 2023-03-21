using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours.Enemy
{
    public class EnemyFlyeyeBehaviour : EnemyBaseBehaviour
    {
        protected override void Init()
        {
            base.Init();
            MonsterSound = "MonsterFlyeye";
        }
    }

}
