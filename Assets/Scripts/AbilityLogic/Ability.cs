using Plugins.MonoCache;

namespace AbilityLogic
{
    public abstract class Ability : MonoCache
    {
        public abstract void Cast();
        public abstract int GetIndexAbility();
    }
}