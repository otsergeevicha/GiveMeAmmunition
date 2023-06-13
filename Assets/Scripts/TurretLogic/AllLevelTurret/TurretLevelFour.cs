using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.AllLevelTurret
{
    public class TurretLevelFour : MonoCache
    {
        [SerializeField] private Transform _leftSpawnPoint;

        private Transform[] _levelFour;
        
        public Transform[] Get() => 
            _levelFour = new[] { _leftSpawnPoint };
    }
}