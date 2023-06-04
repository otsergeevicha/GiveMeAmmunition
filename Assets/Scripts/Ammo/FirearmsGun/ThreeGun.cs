using Plugins.MonoCache;
using UnityEngine;

namespace Ammo.FirearmsGun
{
    public class ThreeGun : MonoCache
    {
        [SerializeField] private Transform _leftOneSpawnPoint;
        [SerializeField] private Transform _rightOneSpawnPoint;
        [SerializeField] private Transform _leftTwoSpawnPoint;
        [SerializeField] private Transform _rightTwoSpawnPoint;
        [SerializeField] private Transform _leftThreeSpawnPoint;
        [SerializeField] private Transform _rightThreeSpawnPoint;
        
        private Transform[] _threeGuns;
        
        public Transform[] Get() => 
            _threeGuns = new[] { _leftOneSpawnPoint, _rightOneSpawnPoint, 
                _leftTwoSpawnPoint, _rightTwoSpawnPoint, 
                _leftThreeSpawnPoint, _rightThreeSpawnPoint};
    }
}