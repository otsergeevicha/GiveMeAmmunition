using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic.AI.States
{
    public class EnemyMovementState : State
    {
        private NavMeshAgent _agent;
        private Transform _target;
        private Animator _animator;

        public EnemyMovementState(NavMeshAgent agent, Transform target, Animator animator)
        {
            _agent = agent;
            _target = target;
            _animator = animator;
        }

        public override void Enable()
        {
            _animator.SetBool(Constants.EnemyWalkHash, true);
            _agent.SetDestination(_target.position);
        }

        public override void Disable()
        {
            _animator.SetBool(Constants.EnemyWalkHash, false);
            _agent.isStopped = true;
        }
    }
}