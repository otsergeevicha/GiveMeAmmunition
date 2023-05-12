using Services.ServiceLocator;

namespace Services.SaveLoadLogic
{
    public interface ISave : IService
    {
        TData Get<TData>();
        void Add<TData>(TData data);
    }
}