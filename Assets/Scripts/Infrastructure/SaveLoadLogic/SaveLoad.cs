using System;
using Services.SaveLoadLogic;
using UnityEngine;

namespace Infrastructure.SaveLoadLogic
{
    public class SaveLoad : ISave
    {
        public SaveLoad()
        {
            Progress = PlayerPrefs.HasKey(Constants.Progress)
                ? JsonUtility.FromJson<Progress>(PlayerPrefs.GetString(Constants.Progress))
                : new Progress();
        }

        public Progress Progress { get; }

        public void Save()
        {
            PlayerPrefs.SetString(Constants.Progress, JsonUtility.ToJson(Progress));
            PlayerPrefs.Save();
        }
    }
}