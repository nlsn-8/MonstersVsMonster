using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Behaviours.Enemy
{
    public class EnemyBatBehaviour : EnemyBaseBehaviour
    {
        protected override void Init()
        {
            base.Init();
            MonsterSound = "MonsterBat";
        }
    }
}
