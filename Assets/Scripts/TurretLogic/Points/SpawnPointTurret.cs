using Infrastructure;
using PlayerLogic;
using Plugins.MonoCache;
using Services.ServiceLocator;
using UnityEngine;

namespace TurretLogic.Points
{
    public class SpawnPointTurret : MonoCache
    {
        [SerializeField] private Transform _vfxFreePlace;
        
        private Turret _turret;
        private IWallet _wallet;

        private void Awake() => 
            _vfxFreePlace.gameObject.SetActive(_turret.Purchased);

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Hero _))
            {
                if (_turret.Purchased) 
                    _turret.Upgrade();

                if (_wallet.Check(Constants.PricePurchaseTurret)) 
                    _turret.Purchase();
            }
        }

        public Transform GetPosition() => 
            transform;

        public void SetTurret(Turret turret) => 
            _turret = turret;

        public void Inject(IWallet wallet) => 
            _wallet = wallet;
    }
}