using System;
using Infrastructure;

namespace TurretLogic
{
    [Serializable]
    public class TurretUpgrade
    {
        private int _currentLevel = (int)TypeTurret.LevelOne;

        public bool GetReady { get; private set; } = false;

        public int LevelUpgrade
        {
            get
            {
                int tempLevel = _currentLevel;
                tempLevel++;

                if (tempLevel <= (int)TypeTurret.LevelFour)
                {
                    _currentLevel = tempLevel;
                    return tempLevel;
                }

                return (int)TypeTurret.LevelFour;
            }
        }

        public int Price 
            => _currentLevel * Constants.UpgradePriceMultiplier;

        public int CurrentLevel => 
            _currentLevel;

        public void SetReady(bool newStatus) =>
            GetReady = newStatus;
    }
}