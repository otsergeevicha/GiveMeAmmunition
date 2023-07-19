using System;
using BehaviorDesigner.Runtime;
using PlayerLogic;

namespace EnemyLogic.EnemyAI.AttackState
{
    [Serializable]
    public class SharedHero : SharedVariable<Hero>
    {
        public static implicit operator SharedHero(Hero value) => 
            new SharedHero { Value = value };
    }
}