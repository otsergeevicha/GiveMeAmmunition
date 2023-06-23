using System;

namespace Infrastructure.SaveLoadLogic
{
    [Serializable]
    public class DataWallet
    {
        public int Read { get; private set; } = 5;

        public void Record(int amountMoney) => 
            Read = amountMoney;
    }
}