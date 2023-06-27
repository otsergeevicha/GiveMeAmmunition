using System;

namespace Infrastructure.SaveLoadLogic
{
    [Serializable]
    public class Progress
    {
        public DataWallet DataWallet;

        public Progress() => 
            DataWallet = new DataWallet();
    }
}