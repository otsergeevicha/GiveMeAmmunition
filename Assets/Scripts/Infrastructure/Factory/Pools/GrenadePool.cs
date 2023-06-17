using Ammo.Ammunition;
using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    public class GrenadePool
    {
        private readonly Grenade[] _grenades = new Grenade[Constants.CountSpawnGrenade];
    
        public GrenadePool(IGameFactory factory)
        {
            for (int i = 0; i < _grenades.Length; i++)
            {
                _grenades[i] = factory.CreateGrenade();
                _grenades[i].gameObject.SetActive(false);
            }
        }

        public Grenade[] GetGrenades() =>
            _grenades;
    }
}