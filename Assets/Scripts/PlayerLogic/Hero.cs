using Infrastructure;
using PlayerLogic.Carrier;
using PlayerLogic.Movements;
using PlayerLogic.Shooting;
using Plugins.MonoCache;
using Services.Health;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    [RequireComponent(typeof(HeroShooting))]
    [RequireComponent(typeof(HeroCarrier))]
    public class Hero : MonoCache, IHealth
    {
        private bool _isLoadedCargo;
        private HeroHealth _health;

        private void Awake() => 
            _health = new HeroHealth(Constants.HeroMaxHealth);

        protected override void OnEnabled() => 
            _health.Died += GameOver;

        protected override void OnDisabled() => 
            _health.Died -= GameOver;

        public void TakeDamage(int damage) => 
            _health.ApplyDamage(damage);

        public bool IsLoaded() =>
            _isLoadedCargo;

        public void SetLoaded(bool newStatus) => 
            _isLoadedCargo = newStatus;

        private void GameOver()
        {
            Time.timeScale = 0;
        }
    }
}