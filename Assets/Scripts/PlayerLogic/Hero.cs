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
        protected bool IsLoadedCargo;

        public void TakeDamage(int damage) {}

        public bool IsLoaded() =>
            IsLoadedCargo;
    }
}