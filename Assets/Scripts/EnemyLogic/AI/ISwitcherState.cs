namespace EnemyLogic.AI
{
    public interface ISwitcherState
    {
        public void Enter();
        public void Exit();
        public void Init(FsmEnemy fsm);
    }
}