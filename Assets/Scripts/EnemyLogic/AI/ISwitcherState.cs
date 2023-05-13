namespace EnemyLogic.AI
{
    public interface ISwitcherState
    {
        void Enable();
        void Disable();
        public void Init(EnemyStateMachine stateMachine);
    }
}