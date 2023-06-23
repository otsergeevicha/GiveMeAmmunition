using System.Collections.Generic;
using Infrastructure;
using Plugins.MonoCache;
using Services.Wallet;
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
        private IWallet _wallet;
        private Transform _upgradeCircle;
        private TurretUpgrade _turretUpgrade;

        public bool Purchased { get; private set; }

        private void Awake()
        {
            _turrets = new Dictionary<int, Transform[]>()
            {
                [(int)TypeTurret.LevelOne] = _turretLevelOne.Get(),
                [(int)TypeTurret.LevelTwo] = _turretLevelTwo.Get(),
                [(int)TypeTurret.LevelThree] = _turretLevelThree.Get(),
                [(int)TypeTurret.LevelFour] = _turretLevelFour.Get()
            };
        }

        public void Construct(Transform getTransform, IWallet wallet)
        {
            _wallet = wallet;
            SetPosition(getTransform);

            _turretUpgrade = new TurretUpgrade();

            SelectorTurret(_turretUpgrade.CurrentLevel);
            
            _wallet.Changed += WalletOnChanged;
        }

        protected override void OnDisabled() => 
            _wallet.Changed -= WalletOnChanged;

        public void TryUpgrade()
        {
            if (_turretUpgrade.GetReady)
            {
                _wallet.Spend(_turretUpgrade.Price);
                SelectorTurret(_turretUpgrade.LevelUpgrade);
            }
        }

        public void Purchase(Transform whiteCircle)
        {
            if (_wallet.Check(Constants.PricePurchaseTurret))
            {
                _wallet.Spend(Constants.PricePurchaseTurret);

                Purchased = true;
                gameObject.SetActive(true);

                if (whiteCircle != null)
                    Destroy(whiteCircle.gameObject);
            }
        }

        public Vector3 GetSpawnPoint(int typeTurret) =>
            _turrets.ContainsKey(typeTurret)
                ? TryGetNextPoint(_turrets[typeTurret])
                : Vector3.zero;

        private void WalletOnChanged(int currentMoney)
        {
            if (currentMoney >= _turretUpgrade.Price)
            {
                _turretUpgrade.SetReady(true);
                _upgradeCircle = Instantiate(_vfxUpgrade, transform.position, Quaternion.identity);
            }
            else
            {
                _turretUpgrade.SetReady(false);
                
                if (_upgradeCircle!=null) 
                    Destroy(_upgradeCircle.gameObject);
            }
        }

        private void SelectorTurret(int typeGun)
        {
            foreach (var value in _turrets)
                value.Value[0].gameObject
                    .SetActive(value.Key == typeGun);
        }

        private void SetPosition(Transform getTransform)
        {
            Transform ourTransform = transform;
            
            ourTransform.position = getTransform.position;
            ourTransform.parent = getTransform;
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