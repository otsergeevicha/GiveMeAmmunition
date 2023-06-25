using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.AllLevelTurret
{
    public class TurretLevelThree : MonoCache
    {
        [SerializeField] private Transform _leftSpawnPoint;
        [SerializeField] private Transform _rightSpawnPoint;
        [SerializeField] private Transform _upSpawnPoint;

        private Transform[] _levelThree;
        
        public Transform[] Get() => 
            _levelThree = new[] { _leftSpawnPoint, _rightSpawnPoint, _upSpawnPoint };
        
        public int GetLevel => 
            (int)TypeTurret.LevelThree;
    }
}