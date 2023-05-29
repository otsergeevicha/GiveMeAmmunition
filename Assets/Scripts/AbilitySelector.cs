using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

enum IndexAbility
{
    Grenade = 0,
    Firearms = 1,
    Flamethrower = 2,
    Shield = 3
}

public class AbilitySelector : MonoCache
{
    [SerializeField] private Ability[] _abilities;
    
    private IInputService _input;

    protected override void OnEnabled() => 
        _input = ServiceRouter.Container.Single<IInputService>();

    public void SelectAbility(int selectIndexAbility)
    {
        for (int i = 0; i < _abilities.Length; i++)
            _abilities[i].gameObject.SetActive(_abilities[i].GetIndexAbility() == selectIndexAbility);
    }

    public Ability[] GetAbilities() =>
        _abilities;
}