using Plugins.MonoCache;
using UnityEngine;

namespace Ammo.FirearmsGun
{
    public class FourGun : MonoCache
    {
        [SerializeField] private Transform _leftOneSpawnPoint;
        [SerializeField] private Transform _rightOneSpawnPoint;
        
        private Transform[] _fourGuns;
        
        public Transform[] Get() => 
            _fourGuns = new[] { _leftOneSpawnPoint, _rightOneSpawnPoint};
    }
}