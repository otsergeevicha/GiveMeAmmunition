using CameraLogic;
using Infrastructure.LoadingLogic;
using Infrastructure.LoadingLogic.ScreenLoading;
using PlayerLogic;
using Services.Factory;
using Services.StateMachine;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            Hero hero = _gameFactory.CreateHero();
            _gameFactory.CreateHud();
            CameraFollow camera = _gameFactory.CreateCamera();

            CameraFollowing(camera, hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollowing(CameraFollow camera, Hero hero) =>
            camera.InitFollowing(hero.ChildrenGet<RootCamera>().transform);
    }
}