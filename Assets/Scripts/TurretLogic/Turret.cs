using System.Collections.Generic;
using Plugins.MonoCache;
using TurretLogic.AllLevelTurret;
using UnityEngine;

namespace TurretLogic
{
    enum TypeTurret
    {
        LevelOne = 0,
        LevelTwo = 1,
        LevelThree = 2,
        LevelFour = 3
    }

    [RequireComponent(typeof(TurretShooting))]
    public class Turret : MonoCache
    {
        [SerializeField] private TurretLevelOne _turretLevelOne;
        [SerializeField] private TurretLevelTwo _turretLevelTwo;
        [SerializeField] private TurretLevelThree _turretLevelThree;
        [SerializeField] private TurretLevelFour _turretLevelFour;
        
        [SerializeField] private Transform _vfxUpgrade;

        private Dictionary<int, Transform[]> _turrets;
        private Vector3 _oldPoint;
        public bool Purchased;

        private void Awake()
        {
            _turrets = new Dictionary<int, Transform[]>()
            {
                [(int)TypeTurret.LevelOne] = _turretLevelOne.Get(),
                [(int)TypeTurret.LevelTwo] = _turretLevelTwo.Get(),
                [(int)TypeTurret.LevelThree] = _turretLevelThree.Get(),
                [(int)TypeTurret.LevelFour] = _turretLevelFour.Get()
            };

            SelectorTurret((int)TypeTurret.LevelOne);
        }

        public Vector3 GetSpawnPoint(int typeTurret) =>
            _turrets.ContainsKey(typeTurret)
                ? TryGetNextPoint(_turrets[typeTurret])
                : Vector3.zero;

        public void SetPosition(Transform getTransform)
        {
            transform.position = getTransform.position;
            transform.parent = getTransform;
        }

        private void SelectorTurret(int typeGun)
        {
            foreach (var value in _turrets)
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

        public void Upgrade()
        {
            throw new System.NotImplementedException();
        }

        public void Purchase()
        {
            throw new System.NotImplementedException();
        }
    }
}