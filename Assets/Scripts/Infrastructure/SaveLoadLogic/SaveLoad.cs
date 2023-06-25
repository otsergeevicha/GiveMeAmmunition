using Services.SaveLoadLogic;
using UnityEngine;

namespace Infrastructure.SaveLoadLogic
{
    public class SaveLoad : ISave
    {
        private readonly Progress _progress;
        
        public SaveLoad()
        {
            _progress = PlayerPrefs.HasKey(Constants.Progress)
                ? JsonUtility.FromJson<Progress>(PlayerPrefs.GetString(Constants.Progress))
                : new Progress();
        }

        public Progress AccessProgress() => 
            _progress;

        public void Save()
        {
            PlayerPrefs.SetString(Constants.Progress, JsonUtility.ToJson(_progress));
            PlayerPrefs.Save();
        }
    }
}