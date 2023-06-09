using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic
{
    public class TurretLevelTwo : MonoCache
    {
        [SerializeField] private Transform _leftSpawnPoint;
        [SerializeField] private Transform _rightSpawnPoint;

        private Transform[] _levelTwo;
        
        public Transform[] Get() => 
            _levelTwo = new[] { _leftSpawnPoint, _rightSpawnPoint };
    }
}