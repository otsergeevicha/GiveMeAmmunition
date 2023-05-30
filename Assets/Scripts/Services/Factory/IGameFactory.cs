using Ammo;
using Ammo.Ammunition;
using Ammo.Pools;
using CameraLogic;
using PlayerLogic;
using Services.ServiceLocator;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        Hero CreateHero();
        void CreateHud();
        CameraFollow CreateCamera();
        void CreateLight();
        Pool CreatePool();
        Grenade CreateGrenade();
        Bullet CreateBullet();
    }
}