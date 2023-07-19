using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace EnemyLogic.EnemyAI.MoveState
{
    public class SetDirection : Action
    {
        public SharedVector2 Direction;

        public override TaskStatus OnUpdate()
        {
            Direction.Value = Vector2.zero;
            return TaskStatus.Success;
        }
    }
}