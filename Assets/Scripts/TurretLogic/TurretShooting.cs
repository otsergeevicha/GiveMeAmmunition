using Ammo.Pools;
using System.Threading;
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

        private void Awake() => 
            _turret = Get<Turret>();

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

        public void Inject(Pool pool) => 
            _pool = pool;

        private async void ImitationQueue(Transform currentTarget)
        {
            while (_isAttack)
            {
                for (int i = 0; i < _turretGun.Length; i++)
                    _turretGun[i].LookAt(currentTarget);

                _pool.TryGetBullet().Shot(_turret.GetSpawnPoint((int)TypeTurret.LevelOne), currentTarget.position);

                await UniTask.Delay(Constants.DelayShotsTurret);
            }
        }
    }
}