using Plugins.MonoCache;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(HeroMovement))]
    public class Hero : MonoCache
    {
        protected bool IsLoadedCargo;
    }
}