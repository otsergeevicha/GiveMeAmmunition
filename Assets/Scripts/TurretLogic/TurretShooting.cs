﻿using System;
using System.Threading;
using Ammo.Pools;
using Cysharp.Threading.Tasks;
using EnemyLogic;
using Infrastructure;
using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic
{
    public class TurretShooting : MonoCache
    {
        [SerializeField] private Transform[] _turretGun;

        private readonly CancellationTokenSource _activeTurretToken = new ();
        
        private Turret _turret;
        private Pool _pool;
        private bool _isAttack;
        private MagazineTurret _magazine;

        private void Awake()
        {
            _turret = Get<Turret>();
            _magazine = new MagazineTurret(Constants.TurretMagazineSize);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!collision.gameObject.TryGetComponent(out Enemy enemy)) 
                return;
            
            _isAttack = true;
            ImitationQueue(enemy.transform);
        }

        private void OnTriggerExit(Collider collision)
        {
            if (!collision.gameObject.TryGetComponent(out Enemy _)) 
                return;
            
            _isAttack = false;
            _activeTurretToken.Cancel();
        }

        public void ApplyAmmo(int newAmmo, Action fulled) => 
            _magazine.ApplyAmmo(newAmmo, () => 
                fulled?.Invoke());

        public void Inject(Pool pool) => 
            _pool = pool;

        public int Shortage() => 
            _magazine.Shortage;

        private async void ImitationQueue(Transform currentTarget)
        {
            while (_isAttack)
            {
                for (int i = 0; i < _turretGun.Length; i++)
                    _turretGun[i].LookAt(currentTarget);

                if (_magazine.Check())
                {
                    _pool.TryGetBullet().Shot(_turret.GetSpawnPoint((int)TypeTurret.LevelOne), currentTarget.position);
                    _magazine.Spend();
                }

                await UniTask.Delay(Constants.DelayShotsTurret);
            }
        }
    }
}