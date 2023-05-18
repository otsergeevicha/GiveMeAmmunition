using UnityEngine;

namespace EnemyLogic.AI.States
{
    public class EnemyIdleState : State
    {
        private Animator _animator;
        
        public EnemyIdleState(Animator animator)
        {
            _animator = animator;
        }

        public override void Enable() => 
            StateMachine.EnterState<EnemyMovementState>();

        public override void Disable() {} 
    }
}