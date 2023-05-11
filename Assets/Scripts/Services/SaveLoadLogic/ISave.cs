using Services.ServiceLocator;

namespace Services.SaveLoadLogic
{
    public interface ISave : IService
    {
        void UpdateDate<TData>(TData data);
    }
}