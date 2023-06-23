using System;

namespace Infrastructure.SaveLoadLogic
{
    [Serializable]
    public class Progress
    {
        public DataWallet DataWallet;
        public DataTurretPool DataTurretPool;

        public Progress()
        {
            DataWallet = new DataWallet();
            DataTurretPool = new DataTurretPool();
        }
    }
}