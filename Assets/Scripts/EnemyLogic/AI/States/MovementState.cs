using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic.AI.States
{
    public class MovementState : State
    {
        private NavMeshAgent _agent;
        private Transform _target;
        private Collider _trigger;

        private CompositeDisposable _disposable = new();

        public MovementState(NavMeshAgent agent, Transform target)
        {
            _agent = agent;
            _target = target;
        }

        public override void Enable()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => { Move(); }).AddTo(_disposable);
        }

        public override void Disable() => 
            _disposable.Clear();

        private void Move()
        {
            if (PurposeNotReached()) 
                _agent.SetDestination(_target.position);
        }

        private bool PurposeNotReached() =>
            Vector3.Distance(_agent.transform.position, _target.position) >= Constants.MinDistanceToTarget;
    }
}