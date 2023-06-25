using System;

namespace Infrastructure.SaveLoadLogic
{
    [Serializable]
    public class DataWallet
    {
        public int Money = 2;

        public int Read() =>
            Money;

        public void Record(int amountMoney) =>
            Money = amountMoney;
    }
}