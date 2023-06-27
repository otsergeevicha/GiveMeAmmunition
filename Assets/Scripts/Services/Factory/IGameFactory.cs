using Ammo.Ammunition;
using CameraLogic;
using EnemyLogic;
using Infrastructure.Factory.Pools;
using PlayerLogic;
using Services.ServiceLocator;
using TurretLogic;
using TurretLogic.Points;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        Hero CreateHero();
        void CreateHud();
        CameraFollow CreateCamera();
        void CreateLight();
        Turret CreateTurret();
        TurretPoints CreateTurretPoints();
        Pool CreatePool();
        Grenade CreateGrenade();
        Bullet CreateBullet();
        Enemy CreateEnemy(string typeEnemy);
    }
}