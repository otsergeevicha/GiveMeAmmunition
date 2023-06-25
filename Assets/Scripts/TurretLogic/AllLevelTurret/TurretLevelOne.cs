using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.AllLevelTurret
{
    public class TurretLevelOne : MonoCache
    {
        [SerializeField] private Transform _spawnPoint;
        
        private Transform[] _levelOne;
        
        public Transform[] Get() => 
            _levelOne = new[] { _spawnPoint};

        public int GetLevel => 
            (int)TypeTurret.LevelOne;
    }
}