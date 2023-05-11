namespace Services.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload>: IExitableState
    {
        void Enter(TPayload payLoad);
    }

    public interface IExitableState
    {
        void Exit();
    }
}