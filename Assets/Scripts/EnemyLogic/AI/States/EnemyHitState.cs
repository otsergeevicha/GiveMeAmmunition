using UnityEngine;

namespace EnemyLogic.AI.States
{
    public class EnemyHitState : State
    {
        private Animator _animator;

        public EnemyHitState(Animator animator) => 
            _animator = animator;

        public override void Enable() =>
            _animator.SetBool(Constants.EnemyHitHash, true);

        public override void Disable() {}

        private void OnHitEnded()
        {
            StateMachine.EnterState<>();
        }
    }
}