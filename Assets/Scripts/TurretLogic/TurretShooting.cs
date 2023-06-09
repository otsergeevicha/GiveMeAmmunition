using Ammo.Pools;
using Cysharp.Threading.Tasks;
using EnemyLogic;
using Infrastructure;
using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic
{
    public class TurretShooting : MonoCache
    {
        [SerializeField] private Transform[] _turretGun;
        
        private Transform _currentTarget;
        private Turret _turret;
        private Pool _pool;

        private void Awake() => 
            _turret = Get<Turret>();

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                _currentTarget = enemy.transform;
                ImitationQueue(Constants.AutomaticQueueTurret);
            }
        }

        public void Inject(Pool pool) => 
            _pool = pool;

        private async void ImitationQueue(int automaticQueue)
        {
            while (automaticQueue != 0)
            {

                for (int i = 0; i < _turretGun.Length; i++) 
                    _turretGun[i].LookAt(_currentTarget);

                _pool.TryGetBullet().Shot(_turret.GetSpawnPoint((int)TypeTurret.LevelOne), _currentTarget.position);
                automaticQueue--;

                await UniTask.Delay(Constants.DelayShotsTurret);
            }
        }
    }
}