using System.Linq;
using Ammo.Ammunition;
using Plugins.MonoCache;
using Services.Factory;
using Services.ServiceLocator;
using TurretLogic;

namespace Ammo.Pools
{
    public class Pool : MonoCache
    {
        private SpawnPointTurret[] _pointTurrets;
        
        private GrenadePool _grenadePool;
        private BulletPool _bulletPool;
        private TurretPool _turretPool;

        private IGameFactory _factory;

        private void Awake()
        {
            _factory = ServiceLocator.Container.Single<IGameFactory>();

            _grenadePool = new GrenadePool(_factory);
            _bulletPool = new BulletPool(_factory);
        }

        public void SetPointTurret(SpawnPointTurret[] pointTurret) => 
            _turretPool = new TurretPool(_factory, pointTurret, this);

        public Bullet TryGetBullet() =>
            _bulletPool.GetBullets().FirstOrDefault(bullet =>
                bullet.isActiveAndEnabled == false);
    
        public Grenade TryGetGrenade() =>
            _grenadePool.GetGrenades().FirstOrDefault(grenade =>
                grenade.isActiveAndEnabled == false);
    }
}