using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnemyLogic;
using Infrastructure;
using Infrastructure.Factory.Pools;
using Infrastructure.Factory.Pools.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WaveLogic
{
    public class LevelWave : IWave
    {
        private readonly CancellationTokenSource _tokenLaunched = new ();
        private readonly WaveConfigurator _config;
        private readonly Transform[] _spawnPoints;
        private readonly Pool _pool;
        
        private bool _isLaunch;

        public LevelWave(int currentLevel, Transform[] spawnPoints, Pool pool)
        {
            _pool = pool;
            _spawnPoints = spawnPoints;
            _config = new WaveConfigurator(currentLevel, OnLoaded);
        }

        private void OnLoaded()
        {
            if (_isLaunch) 
                _tokenLaunched.Cancel();

            _pool.CreateEnemiesPool(_config.Get.OneTypeEnemy, _config.Get.TwoTypeEnemy, _config.Get.ThreeTypeEnemy);
            
            _isLaunch = true;

            LaunchLevel();
        }

        private async void LaunchLevel()
        {
            float currentTime = _config.Get.TimeLevel;
            
            while (_isLaunch)
            {
                currentTime -= Time.deltaTime;

                if (currentTime <= Single.Epsilon)
                    _isLaunch = false;

                Spawn();

                await UniTask.Delay(Constants.DelaySpawnEnemy);
            }
        }

        private void Spawn()
        {
            Enemy enemy = _pool.TryGetEnemy(Random.Range((int)TypeEnemy.One, (int)TypeEnemy.Three));
            enemy.gameObject.SetActive(true);
            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)].position;
        }
    }
}