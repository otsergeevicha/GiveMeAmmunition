using UnityEngine;

namespace Infrastructure.SaveLoadLogic
{
    public class DataBase
    {
        public void Set<TData>(TData data)
        {
            DataWrapper<TData> wrapper = new DataWrapper<TData>(data);

            PlayerPrefs.SetString(typeof(TData).FullName, JsonUtility.ToJson(wrapper));
            PlayerPrefs.Save();
        }

        public TData Get<TData>()
        {
            string serializeObject = PlayerPrefs.GetString(typeof(TData).FullName);

            if (serializeObject == null)
                return default(TData);

            DataWrapper<TData> wrapper = JsonUtility.FromJson<DataWrapper<TData>>(serializeObject);

            return wrapper == null 
                ? default(TData) 
                : wrapper.Value;
        }
    }
}