namespace EnemyLogic.AI.States
{
    public class EnemyIdleState : State
    {
        public override void Enable() => 
            StateMachine.EnterState<EnemyMovementState>();

        public override void Disable() {} 
    }
}