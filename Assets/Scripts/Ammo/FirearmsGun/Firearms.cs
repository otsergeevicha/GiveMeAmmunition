using System.Collections.Generic;
using Plugins.MonoCache;
using UnityEngine;

namespace Ammo.FirearmsGun
{
    enum TypeGun
    {
        OneGun = 0,
        TwoGun = 1,
        ThreeGun = 2,
        FourGun = 3
    }
    public class Firearms : MonoCache
    {
        [SerializeField] private OneGun _oneGun;
        [SerializeField] private TwoGun _twoGun;
        [SerializeField] private ThreeGun _threeGun;
        [SerializeField] private FourGun _fourGun;

        private Dictionary<int, Transform[]> _guns;
        private Vector3 _oldPoint;

        private void Awake()
        {
            _guns = new Dictionary<int, Transform[]>()
            {
                [(int)TypeGun.OneGun] = _oneGun.Get(),
                [(int)TypeGun.TwoGun] = _twoGun.Get(),
                [(int)TypeGun.ThreeGun] = _threeGun.Get(),
                [(int)TypeGun.FourGun] = _fourGun.Get()
            };
            
            SelectorGuns((int)TypeGun.OneGun);
        }

        public Vector3 GetSpawnPoint(int typeGun) => 
            _guns.ContainsKey(typeGun) 
                ? TryGetNextPoint(_guns[typeGun]) 
                : Vector3.zero;

        private void SelectorGuns(int typeGun)
        {
            foreach (var value in _guns) 
                value.Value[0].gameObject
                    .SetActive(value.Key == typeGun);
        }

        private Vector3 TryGetNextPoint(Transform[] spawnPoints)
        {
            Vector3 currentPoint = CurrentPoint(spawnPoints);
            _oldPoint = currentPoint;
            return currentPoint;
        }

        private Vector3 CurrentPoint(Transform[] spawnPoints) => 
            spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}