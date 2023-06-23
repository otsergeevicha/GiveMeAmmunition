using AbilityLogic;
using Ammo;
using CameraLogic;
using Infrastructure.Factory.Pools;
using Infrastructure.LoadingLogic;
using Infrastructure.LoadingLogic.ScreenLoading;
using PlayerLogic;
using Services.Factory;
using Services.StateMachine;
using Services.Wallet;
using TurretLogic.Points;

namespace Infrastructure.GameAI.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IWallet _wallet;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IWallet wallet)
        {
            _wallet = wallet;
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
            _gameFactory.CreateLight();
            Hero hero = _gameFactory.CreateHero();
            _gameFactory.CreateHud();
            CameraFollow camera = _gameFactory.CreateCamera();
            CameraFollowing(camera, hero);
            Pool pool = _gameFactory.CreatePool();
            InjectPool(hero, pool, camera);
            SpawnPointTurret[] spawnPointTurret = _gameFactory.CreateTurretPoints().Get();
            pool.InjectDependence(spawnPointTurret, _wallet);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void InjectPool(Hero hero, Pool pool, CameraFollow camera)
        {
            hero.ChildrenGet<WeaponContainer>().Inject(camera);
            hero.ChildrenGet<FirearmsAbility>().Inject(pool, camera);
            hero.ChildrenGet<FlamethrowerAbility>().Inject(camera);
            hero.ChildrenGet<GrenadeAbility>().Construct(pool, camera);
        }

        private void CameraFollowing(CameraFollow camera, Hero hero) =>
            camera.InitFollowing(hero.ChildrenGet<RootCamera>().transform);
    }
}