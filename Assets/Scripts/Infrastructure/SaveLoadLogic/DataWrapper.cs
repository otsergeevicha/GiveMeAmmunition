namespace Infrastructure.SaveLoadLogic
{
    public class DataWrapper<TData>
    {
        public TData Value;

        public DataWrapper(TData value) => 
            Value = value;
    }
}