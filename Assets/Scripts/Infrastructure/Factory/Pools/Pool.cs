using System.Linq;
using Ammo.Ammunition;
using Plugins.MonoCache;
using Services.Factory;
using Services.SaveLoadLogic;
using Services.ServiceLocator;
using Services.Wallet;
using TurretLogic.Points;

namespace Infrastructure.Factory.Pools
{
    public class Pool : MonoCache
    {
        private SpawnPointTurret[] _pointTurrets;
        
        private GrenadePool _grenadePool;
        private BulletPool _bulletPool;
        private TurretPool _turretPool;

        private IGameFactory _factory;
        private ISave _save;

        private void Awake()
        {
            _factory = ServiceLocator.Container.Single<IGameFactory>();
            _save = ServiceLocator.Container.Single<ISave>();
            
            _grenadePool = new GrenadePool(_factory);
            _bulletPool = new BulletPool(_factory);
        }

        protected override void OnDisabled() => 
            _save.Add(_turretPool);

        public void InjectDependence(SpawnPointTurret[] pointTurret, IWallet wallet)
        {
            _turretPool = GetCurrentTurretPool();
            _turretPool.InjectDependence(_factory, pointTurret, this, wallet, _save);
        }

        public Bullet TryGetBullet() =>
            _bulletPool.GetBullets().FirstOrDefault(bullet =>
                bullet.isActiveAndEnabled == false);

        public Grenade TryGetGrenade() =>
            _grenadePool.GetGrenades().FirstOrDefault(grenade =>
                grenade.isActiveAndEnabled == false);

        private TurretPool GetCurrentTurretPool() => 
            _save.Get<TurretPool>() ?? new TurretPool();
    }
}