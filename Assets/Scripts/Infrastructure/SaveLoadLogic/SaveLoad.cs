using Services.SaveLoadLogic;
using UnityEngine;

namespace Infrastructure.SaveLoadLogic
{
    public class SaveLoad : ISave
    {
        private DataBase _dataBase;

        public SaveLoad()
        {
            _dataBase = PlayerPrefs.HasKey(Constants.Key)
                ? JsonUtility.FromJson<DataBase>(PlayerPrefs.GetString(Constants.Key))
                : new DataBase();
        }

        public TData TryGetData<TData>(TData data)
        {
            return _dataBase.ActualData
                .TryGetValue(ConvertToJson(data), out string actualData) 
                ? JsonUtility.FromJson<TData>(actualData) 
                : data;
        }
        
        public void UpdateDate<TData>(TData data)
        {
            _dataBase.ActualData.Add(ConvertToJson(data));
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetString(Constants.Key, ConvertToJson(_dataBase));
            PlayerPrefs.Save();
        }

        private string ConvertToJson<TData>(TData data) => 
            JsonUtility.ToJson(data);
    }
}