using System.Threading;
using AmmoRepository;
using Cysharp.Threading.Tasks;
using Infrastructure;
using TurretLogic;
using UnityEngine;

namespace PlayerLogic.Carrier
{
    public class HeroCarrier : Hero
    {
        private readonly CancellationTokenSource _tokenReplenishment = new();
        private HeroBasket _basket;
        private bool _isReplenishment;

        private void Awake() =>
            _basket = new HeroBasket(Constants.SizeHeroBasket);

        protected override void OnEnabled() => 
            _basket.IsEmpty += ChangeStatusCarrier;

        protected override void OnDisabled() => 
            _basket.IsEmpty -= ChangeStatusCarrier;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out AmmoPoint ammoPoint) 
                && _basket.IsReplenishmentRequired())
            {
                _isReplenishment = true;
                ReplenishmentBasket(ammoPoint);
            }

            if (collision.TryGetComponent(out TurretShooting turret))
            {
                if (_basket.Cartridge != 0)
                {
                    _isReplenishment = true;
                    ReplenishingAmmoTurret(turret);
                }
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out AmmoPoint _))
            {
                _isReplenishment = false;
                CancelToken();
            }
        }

        private void ChangeStatusCarrier() => 
            IsLoadedCargo = false;

        private async void ReplenishingAmmoTurret(TurretShooting turret)
        {
            while (_isReplenishment)
            {
                turret.ApplyAmmo(_basket.GetAmmo(Constants.AmountAmmo), delegate
                {
                    _isReplenishment = false;
                    CancelToken();
                });
                
                await UniTask.Delay(Constants.AmmunitionDeliveryRate);
            }
        }

        private async void ReplenishmentBasket(AmmoPoint depot)
        {
            while (_isReplenishment)
            {
                _basket.ApplyAmmo(depot.GetAmmo(), delegate
                {
                    _isReplenishment = false;
                    IsLoadedCargo = true;
                    CancelToken();
                });

                await UniTask.Delay(Constants.AmmunitionDeliveryRate);
            }
        }

        private void CancelToken() => 
            _tokenReplenishment.Cancel();
    }
}