using System.Threading;
using AmmoRepository;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Plugins.MonoCache;
using TurretLogic;
using TurretLogic.Points;
using UnityEngine;

namespace PlayerLogic.Carrier
{
    [RequireComponent(typeof(Hero))]
    public class HeroCarrier : MonoCache
    {
        private readonly CancellationTokenSource _tokenReplenishment = new();
        private HeroBasket _basket;
        private bool _isReplenishment;
        private Hero _hero;

        private void Awake()
        {
            _basket = new HeroBasket(Constants.SizeHeroBasket);
            _hero = Get<Hero>();
        }

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
                _hero.SetLoaded(true);
                ReplenishmentBasket(ammoPoint);
            }

            if (collision.TryGetComponent(out TurretPointTrigger trigger))
            {
                TurretShooting turret = trigger.GetTurret().Get<TurretShooting>();
            
                if (turret.RequiredApply() && _basket.Cartridge != 0)
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
                OffReplenishment();
                CancelToken();
            }
        }

        private void ChangeStatusCarrier() =>
            _hero.SetLoaded(false);

        private async void ReplenishingAmmoTurret(TurretShooting turret)
        {
            while (_isReplenishment)
            {
                turret.ApplyAmmo(_basket.GetAmmo(Constants.AmountAmmo), Fulled);
                await UniTask.Delay(Constants.AmmunitionDeliveryRate);
            }
        }

        private async void ReplenishmentBasket(AmmoPoint depot)
        {
            while (_isReplenishment)
            {
                _basket.ApplyAmmo(depot.GetAmmo(), Fulled);
                await UniTask.Delay(Constants.AmmunitionDeliveryRate);
            }
        }

        private void Fulled()
        {
            OffReplenishment();
            CancelToken();
        }

        private void OffReplenishment() =>
            _isReplenishment = false;

        private void CancelToken() =>
            _tokenReplenishment.Cancel();
    }
}