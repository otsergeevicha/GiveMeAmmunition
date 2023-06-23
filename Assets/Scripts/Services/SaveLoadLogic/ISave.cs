using Infrastructure.SaveLoadLogic;
using Services.ServiceLocator;

namespace Services.SaveLoadLogic
{
    public interface ISave : IService
    {
        Progress Progress { get; }
        void Save();
    }
}