using CameraLogic;
using PlayerLogic;
using Services.ServiceLocator;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        Hero CreateHero();
        void CreateHud();
        CameraFollow CreateCamera();
    }
}