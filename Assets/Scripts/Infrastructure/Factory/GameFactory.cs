using CameraLogic;
using PlayerLogic;
using Services.Assets;
using Services.Factory;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public GameFactory(IAssetsProvider assetsProvider) => 
            _assetsProvider = assetsProvider;

        public Hero CreateHero() => 
            _assetsProvider.InstantiateEntity(Constants.PlayerPath)
                .GetComponent<Hero>();

        public void CreateHud() => 
            _assetsProvider.InstantiateEntity(Constants.HudPath);
        
        public CameraFollow CreateCamera() => 
            _assetsProvider.InstantiateEntity(Constants.CameraPath)
                .GetComponent<CameraFollow>();

        public void CreateLight() =>
            _assetsProvider.InstantiateEntity(Constants.LightPath);

        public Pool CreatePool() =>
            _assetsProvider.InstantiateEntity(Constants.PoolPath)
                .GetComponent<Pool>();

        public Grenade CreateGrenade() =>
            _assetsProvider.InstantiateEntity(Constants.GrenadePath).GetComponent<Grenade>();
        
        public Bullet CreateBullet() =>
            _assetsProvider.InstantiateEntity(Constants.BulletPath).GetComponent<Bullet>();
    }
}