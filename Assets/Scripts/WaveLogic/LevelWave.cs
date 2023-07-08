using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using EnemyLogic;
using Infrastructure;
using Infrastructure.Factory.Pools;
using Infrastructure.Factory.Pools.Enemies;
using UnityEngine;

namespace WaveLogic
{
    public class LevelWave : IWave
    {
        private readonly CancellationTokenSource _tokenLaunched = new();
        private readonly WaveConfigurator _waveConfigurator;
        private readonly Transform[] _spawnPoints;
        private readonly Pool _pool;

        private Vector3 _currentPosition;
        private int _requiredTypeEnemy;
        private int _lastActivePortal = 0;

        public LevelWave(int currentLevel, Transform[] spawnPoints, Pool pool)
        {
            _pool = pool;
            _spawnPoints = spawnPoints;
            _waveConfigurator = new WaveConfigurator(currentLevel);
            OnLoaded();
        }

        private void OnLoaded()
        {
            _pool.CreateEnemiesPool(_waveConfigurator.Get().OneTypeEnemy(), _waveConfigurator.Get().TwoTypeEnemy(),
                _waveConfigurator.Get().ThreeTypeEnemy());
            _requiredTypeEnemy = (int)TypeEnemy.One;
            _ = StartSpawning();
        }

        private async UniTaskVoid StartSpawning()
        {
            float delayOpenNewPortal = DelayOpenNewPortal();
            float portalTimer = delayOpenNewPortal;

            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate())
            {
                portalTimer -= Time.deltaTime;

                if (portalTimer <= float.Epsilon) 
                    portalTimer = SpawnPortal(portalTimer, delayOpenNewPortal);

                await TryGetSpawnPoint();
            }
        }

        private float SpawnPortal(float portalTimer, float delayOpenNewPortal)
        {
            portalTimer += delayOpenNewPortal;

            _lastActivePortal++;

            if (_lastActivePortal == _spawnPoints.Length)
                _lastActivePortal = _spawnPoints.Length - 1;

            _spawnPoints[_lastActivePortal].gameObject.SetActive(true);
            return portalTimer;
        }

        private float DelayOpenNewPortal() => 
            _waveConfigurator.Get().TimeLevel() / _spawnPoints.Length;

        private async UniTask SpawnEnemy()
        {
            Enemy enemy = _pool.TryGetEnemy(_requiredTypeEnemy);

            if (enemy != null)
            {
                await UniTask.Delay(Constants.DelaySpawnEnemy);
                enemy.gameObject.GetComponent<Enemy>().OnActive(_currentPosition);

                return;
            }

            if (enemy == null)
                _requiredTypeEnemy++;
        }

        private async UniTask TryGetSpawnPoint()
        {
            Transform currentPosition = GetPoint();

            if (currentPosition.gameObject.activeInHierarchy)
            {
                if (currentPosition.position != Vector3.zero)
                {
                    _currentPosition = currentPosition.position;
                    await SpawnEnemy();
                    return;
                }

                await UniTask.DelayFrame(1);
            }
        }

        private Transform GetPoint() =>
            _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}