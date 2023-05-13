namespace EnemyLogic.AI.States
{
    public class IdleState : State
    {
        public override void Enable() => 
            StateMachine.EnterState<MovementState>();

        public override void Disable() {}
    }
}