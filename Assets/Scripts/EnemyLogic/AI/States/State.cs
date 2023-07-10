using Plugins.MonoCache;

namespace EnemyLogic.AI.States
{
    public abstract class State : MonoCache, ISwitcherState
    {
        protected FsmEnemy Fsm;

        public void Enter() =>
            enabled = true;

        public void Exit() =>
            enabled = false;

        public void Init(FsmEnemy fsm) =>
            Fsm = fsm;
    }
}