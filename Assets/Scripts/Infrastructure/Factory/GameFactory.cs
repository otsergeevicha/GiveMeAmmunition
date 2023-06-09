﻿using Ammo.Ammunition;
using CameraLogic;
using EnemyLogic;
using Infrastructure.Factory.Pools;
using PlayerLogic;
using Services.Assets;
using Services.Factory;
using TurretLogic;
using TurretLogic.Points;

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

        public Turret CreateTurret() =>
            _assetsProvider.InstantiateEntity(Constants.TurretPath)
                .GetComponent<Turret>();

        public TurretPoints CreateTurretPoints() =>
            _assetsProvider.InstantiateEntity(Constants.TurretPointsPath)
                .GetComponent<TurretPoints>();

        public Pool CreatePool() =>
            _assetsProvider.InstantiateEntity(Constants.PoolPath)
                .GetComponent<Pool>();

        public Grenade CreateGrenade() =>
            _assetsProvider.InstantiateEntity(Constants.GrenadePath)
                .GetComponent<Grenade>();
        
        public Bullet CreateBullet() =>
            _assetsProvider.InstantiateEntity(Constants.BulletPath)
                .GetComponent<Bullet>();

        public Enemy CreateEnemy(string typeEnemy) => 
            _assetsProvider.InstantiateEntity(typeEnemy)
                .GetComponent<Enemy>();

        public EnemyPortals CreateEnemyPortals() => 
            _assetsProvider.InstantiateEntity(Constants.EnemyPortalsPath)
                .GetComponent<EnemyPortals>();
    }
}