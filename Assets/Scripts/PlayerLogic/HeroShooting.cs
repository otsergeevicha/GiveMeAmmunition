using System.Linq;
using AbilityLogic;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic
{
    public class HeroShooting : Hero
    {
        [SerializeField] private AbilitySelector _abilitySelector;
        
        private IInputService _input;

        private void Awake()
        {
            _input = ServiceLocator.Container.Single<IInputService>();
            _input.PushShoot(OnShoot);
        }

        private void OnShoot() =>
            TryGetAbility().Cast();

        private Ability TryGetAbility() =>
            _abilitySelector.GetAbilities().FirstOrDefault(ability =>
                ability.isActiveAndEnabled);
    }
}