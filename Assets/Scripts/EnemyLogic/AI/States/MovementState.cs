using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic.AI.States
{
    public class MovementState : State
    {
        private NavMeshAgent _agent;
        private Transform _target;
        private Collider _trigger;

        public MovementState(NavMeshAgent agent, Transform target)
        {
            _agent = agent;
            _target = target;
        }

        public override void Enable() => 
            Move();

        public override void Disable() {}

        private async void Move()
        {
            while (_agent != null)
            {
                if (PurposeNotReached())
                    _agent.SetDestination(_target.position);
                
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private bool PurposeNotReached() =>
            Vector3.Distance(_agent.transform.position, _target.position) >= Constants.MinDistanceToTarget;
    }
}