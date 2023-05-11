using Infrastructure.Assets;
using Infrastructure.Factory;
using Infrastructure.LoadingLogic;
using Infrastructure.SaveLoadLogic;
using Services.Assets;
using Services.Factory;
using Services.Inputs;
using Services.SaveLoadLogic;
using Services.ServiceLocator;
using Services.StateMachine;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Constants.InitialScene, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            AllServices.Container.RegisterSingle<ISave>(new SaveLoad());
            AllServices.Container.RegisterSingle<IInputService>(new InputService());
            AllServices.Container.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetsProvider>()));
        }
    }
}