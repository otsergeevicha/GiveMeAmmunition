using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnemyLogic;
using Infrastructure;
using Infrastructure.Factory.Pools;
using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic
{
    public class TurretShooting : MonoCache
    {
        [SerializeField] private Transform[] _turretGun;
        
        [SerializeField] private Transform _redCircle;
        [SerializeField] private Transform _greenCircle;

        private readonly CancellationTokenSource _activeTurretToken = new ();
        
        private Turret _turret;
        private Pool _pool;
        private bool _isAttack;
        private MagazineTurret _magazine;
        private Enemy _enemy;

        private void Awake()
        {
            _turret = Get<Turret>();
            _magazine = new MagazineTurret(Constants.TurretMagazineSize);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                _isAttack = true;
                _enemy = enemy;
                _ = ImitationQueue(enemy.transform);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy _))
            {
                _isAttack = false;
                _enemy = null;
                _activeTurretToken.Cancel();
            }
        }

        protected override void UpdateCached()
        {
            if (_isAttack) 
                RotateTurret(_enemy.transform);
        }

        public void ApplyAmmo(int newAmmo, Action fulled) => 
            _magazine.ApplyAmmo(newAmmo, () => 
                fulled?.Invoke());

        public void Inject(Pool pool) => 
            _pool = pool;

        private async UniTaskVoid ImitationQueue(Transform currentTarget)
        {
            while (_isAttack)
            {
                if (_magazine.Check())
                {
                    _pool.TryGetBullet().Shot(_turret.GetSpawnPoint((int)TypeTurret.LevelOne), currentTarget.position);
                    _magazine.Spend();
                }

                await UniTask.Delay(Constants.DelayShotsTurret);
            }
        }

        private void RotateTurret(Transform currentTarget)
        {
            for (int i = 0; i < _turretGun.Length; i++)
                _turretGun[i].LookAt(currentTarget);
        }

        public bool RequiredApply() => 
            _magazine.CheckFull && gameObject.activeInHierarchy;
    }
}