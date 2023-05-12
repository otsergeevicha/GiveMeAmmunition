using System;
using System.Collections.Generic;
using Services.SaveLoadLogic;

namespace Infrastructure.SaveLoadLogic
{
    public class SaveLoad : ISave
    {
        private readonly Dictionary<Type, object> _datas = new ();
        private readonly DataBase _dataBase;

        public SaveLoad() : this(new DataBase()) {}
        
        public SaveLoad(DataBase dataBase) => 
            _dataBase = dataBase;

        public TData Get<TData>()
        {
            if (_datas.ContainsKey(typeof(TData)))
                return (TData)_datas[typeof(TData)];

            return _dataBase.Get<TData>();
        }

        public void Add<T>(T data)
        {
            _datas[typeof(T)] = data;

            _dataBase.Set(data);
        }
    }
}