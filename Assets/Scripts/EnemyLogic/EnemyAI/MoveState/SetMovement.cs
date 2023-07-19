using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using EnemyLogic.EnemyAI.AttackState;
using Infrastructure;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic.EnemyAI.MoveState
{
    public class SetMovement : Action
    {
        public SharedBotInput SelfBotInput;
        public SharedVector2 Direction;

        private IBotInput _inputSource;
        private NavMeshAgent _agent;

        public override void OnAwake()
        {
            _inputSource = GetComponent<BotInput>();
            _agent = GetComponent<NavMeshAgent>();
            
            _agent.speed = Constants.EnemySpeed;
        }

        public override void OnStart()
        {
            SelfBotInput.Value.MovementInput = Direction.Value;
            _agent.SetDestination(new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y));
        }
    }
}