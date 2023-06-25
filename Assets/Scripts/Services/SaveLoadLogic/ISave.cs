using Services.ServiceLocator;
using Infrastructure.SaveLoadLogic;

namespace Services.SaveLoadLogic
{
    public interface ISave : IService
    {
        Progress AccessProgress();
        void Save();
    }
}