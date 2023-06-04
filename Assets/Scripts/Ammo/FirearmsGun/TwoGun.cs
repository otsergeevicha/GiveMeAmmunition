using Plugins.MonoCache;
using UnityEngine;

namespace Ammo.FirearmsGun
{
    public class TwoGun : MonoCache
    {
        [SerializeField] private Transform _leftOneSpawnPoint;
        [SerializeField] private Transform _rightOneSpawnPoint;
        [SerializeField] private Transform _leftTwoSpawnPoint;
        [SerializeField] private Transform _rightTwoSpawnPoint;
        
        private Transform[] _twoGuns;
        
        public Transform[] Get() => 
            _twoGuns = new[] { _leftOneSpawnPoint, _rightOneSpawnPoint, 
                _leftTwoSpawnPoint, _rightTwoSpawnPoint};
    }
}