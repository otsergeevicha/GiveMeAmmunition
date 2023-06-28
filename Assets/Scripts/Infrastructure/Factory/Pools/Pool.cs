using System.Linq;
using Ammo.Ammunition;
using EnemyLogic;
using Plugins.MonoCache;
using Services.Factory;
using Services.ServiceLocator;
using Services.Wallet;
using TurretLogic.Points;

namespace Infrastructure.Factory.Pools
{
    public class Pool : MonoCache
    {
        private GrenadePool _grenadePool;
        private BulletPool _bulletPool;
        private TurretPool _turretPool;
        private EnemiesPool _enemiesPool;

        private IGameFactory _factory;

        private void Awake()
        {
            _factory = ServiceLocator.Container.Single<IGameFactory>();
            
            _grenadePool = new GrenadePool(_factory);
            _bulletPool = new BulletPool(_factory);
        }

        public void InjectDependence(SpawnPointTurret[] pointTurret, IWallet wallet) => 
            _turretPool = new TurretPool(_factory, pointTurret, this, wallet);

        public void CreateEnemiesPool(string getOneTypeEnemy, string getTwoTypeEnemy, string getThreeTypeEnemy) => 
            _enemiesPool = new EnemiesPool(_factory, getOneTypeEnemy, getTwoTypeEnemy, getThreeTypeEnemy);

        public Bullet TryGetBullet() =>
            _bulletPool.GetBullets().FirstOrDefault(bullet =>
                bullet.isActiveAndEnabled == false);

        public Grenade TryGetGrenade() =>
            _grenadePool.GetGrenades().FirstOrDefault(grenade =>
                grenade.isActiveAndEnabled == false);

        public Enemy TryGetEnemy(int typeEnemy) =>
            _enemiesPool.TryGet(typeEnemy);
    }
}