using Infrastructure.Factory.Pools;
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
}