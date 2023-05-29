using Plugins.MonoCache;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    public class Hero : MonoCache, IHealth
    {
        protected bool IsLoadedCargo;

        public void TakeDamage(int damage) {}
    }
}