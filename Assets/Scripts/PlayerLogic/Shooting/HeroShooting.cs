using System.Linq;
using AbilityLogic;
using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic.Shooting
{
    public class HeroShooting : MonoCache
    {
        [SerializeField] private AbilitySelector _abilitySelector;

        private void Awake()
        {
            IInputService input = ServiceLocator.Container.Single<IInputService>();
            input.PushShoot(OnShoot);
        }

        private void OnShoot() =>
            TryGetAbility().Cast();

        private Ability TryGetAbility() =>
            _abilitySelector.GetAbilities().FirstOrDefault(ability =>
                ability.isActiveAndEnabled);
    }
}