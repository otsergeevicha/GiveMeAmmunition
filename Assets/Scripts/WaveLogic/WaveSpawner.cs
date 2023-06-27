using System;
using System.Threading;
using EnemyLogic;
using Infrastructure;
using Infrastructure.Factory.Pools;
using Services.Factory;
using UnityEngine;

namespace WaveLogic
{
    enum IndexLevel
    {
        One,
        Two,
        Three
    }
    public class WaveSpawner
    {
        private IWave _level;

        public WaveSpawner(int currentLevel, Transform[] spawnPoints, Pool pool) => 
            _level = new LevelWave(currentLevel, spawnPoints, pool);
    }

    public interface IWave {}
    
    public class LevelWave : IWave
    {
        private readonly CancellationTokenSource _tokenLaunched = new ();
        private readonly WaveConfigurator _config;
        
        private Transform[] _spawnPoints;
        private bool _isLaunch;
        private Pool _pool;

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

            _pool.CreateEnemies(_config.Get.OneTypeEnemy, _config.Get.TwoTypeEnemy, _config.Get.ThreeTypeEnemy);
            
            _isLaunch = true;

            LaunchLevel(_pool.TryGetEnemy());
        }

        private async void LaunchLevel(Enemy enemy)
        {
            
        }
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public class WaveConfigurator
    {
        private readonly DataWave _dataWaves = new ();
        
        public WaveConfigurator(int currentLevel, Action onLoaded)
        {
            switch (currentLevel)
            {
                case (int)IndexLevel.One:
                    LevelOne((int)IndexLevel.One);
                    Notify(onLoaded);
                    break;
                
                case (int)IndexLevel.Two:
                    LevelTwo((int)IndexLevel.Two);
                    Notify(onLoaded);
                    break;
                
                case (int)IndexLevel.Three:
                    LevelThree((int)IndexLevel.Three);
                    Notify(onLoaded);
                    break;
                default:
                    LevelOne((int)IndexLevel.One);
                    Notify(onLoaded);
                    break;
            }
        }

        public DataWave Get =>
            _dataWaves;

        private void LevelOne(int currentLevel) =>
            _dataWaves.InjectDependency(currentLevel, Constants.TimeLevelOne,
                Constants.TurtlePath, Constants.SlimePath, Constants.SpiderPath);

        private void LevelTwo(int currentLevel) =>
            _dataWaves.InjectDependency(currentLevel, Constants.TimeLevelTwo,
                Constants.BatPath, Constants.EvilMagePath, Constants.DragonPath);

        private void LevelThree(int currentLevel) =>
            _dataWaves.InjectDependency(currentLevel, Constants.TimeLevelThree,
                Constants.GolemPath, Constants.MonsterPlantPath, Constants.OrcPath);

        private void Notify(Action onLoaded) => 
            onLoaded?.Invoke();
    }

    public class DataWave
    {
        public void InjectDependency(int currentLevel, float timeLevel, 
            string oneTypeEnemy, string twoTypeEnemy, string threeTypeEnemy)
        {
            ThreeTypeEnemy = threeTypeEnemy;
            TwoTypeEnemy = twoTypeEnemy;
            OneTypeEnemy = oneTypeEnemy;
            
            TimeLevel = timeLevel;
            CurrentLevel = currentLevel;
        }

        public float TimeLevel { get; private set; }
        public int CurrentLevel { get; private set; }
        public string OneTypeEnemy { get; private set; }
        public string TwoTypeEnemy { get; private set; }
        public string ThreeTypeEnemy { get; private set; }
    }
}