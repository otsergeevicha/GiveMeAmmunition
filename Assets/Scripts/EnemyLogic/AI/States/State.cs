namespace EnemyLogic.AI.States
{
    public abstract class State : ISwitcherState
    {
        protected EnemyStateMachine StateMachine;

        public abstract void Enable();

        public abstract void Disable();

        public void Init(EnemyStateMachine stateMachine) =>
            StateMachine = stateMachine;
    }
}