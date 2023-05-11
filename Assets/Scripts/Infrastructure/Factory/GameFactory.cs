using Services.Assets;
using Services.Factory;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public GameFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public GameObject CreateHero() => 
            _assetsProvider.InstantiateEntity(Constants.PlayerPath);

        public void CreateGud() => 
            _assetsProvider.InstantiateEntity(Constants.HudPath);
    }
}