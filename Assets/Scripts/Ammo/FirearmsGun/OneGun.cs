using Plugins.MonoCache;
using UnityEngine;

namespace Ammo.FirearmsGun
{
    public class OneGun : MonoCache
    {
        [SerializeField] private Transform _leftOneSpawnPoint;
        [SerializeField] private Transform _rightOneSpawnPoint;

        private Transform[] _oneGuns;
        
        public Transform[] Get() => 
            _oneGuns = new[] { _leftOneSpawnPoint, _rightOneSpawnPoint };
    }
}