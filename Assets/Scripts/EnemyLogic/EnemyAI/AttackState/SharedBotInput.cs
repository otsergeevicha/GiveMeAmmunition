using System;
using BehaviorDesigner.Runtime;
using EnemyLogic.EnemyAI.MoveState;

namespace EnemyLogic.EnemyAI.AttackState
{
    [Serializable]
    public class SharedBotInput : SharedVariable<BotInput>
    {
        public static implicit operator SharedBotInput(BotInput value) => 
            new SharedBotInput { Value = value };
    }
}