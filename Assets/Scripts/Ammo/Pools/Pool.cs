using System.Linq;
using Ammo.Ammunition;
using Plugins.MonoCache;
using Services.Factory;
using Services.ServiceLocator;

namespace Ammo.Pools
{
    public class Pool : MonoCache
    {
        private GrenadePool _grenadePool;
        private BulletPool _bulletPool;

        private void Awake()
        {
            IGameFactory factory = ServiceRouter.Container.Single<IGameFactory>();

            _grenadePool = new GrenadePool(factory);
            _bulletPool = new BulletPool(factory);
        }
    
        public Bullet TryGetBullet() =>
            _bulletPool.GetBullets().FirstOrDefault(bullet =>
                bullet.isActiveAndEnabled == false);
    
        public Grenade TryGetGrenade() =>
            _grenadePool.GetGrenades().FirstOrDefault(grenade =>
                grenade.isActiveAndEnabled == false);
    }
}