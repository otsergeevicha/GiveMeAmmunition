using Services.ServiceLocator;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero();
        void CreateGud();
    }
}