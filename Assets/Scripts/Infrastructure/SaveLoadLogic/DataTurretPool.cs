using System;
using Infrastructure.Factory.Pools;

namespace Infrastructure.SaveLoadLogic
{
    [Serializable]
    public class DataTurretPool
    {
        public TurretData[] TurretDatas = new TurretData[0];

        public TurretData[] Read() =>
            TurretDatas;

        public void Record(TurretData[] turretDatas) =>
            TurretDatas = turretDatas;

        public bool Check() =>
            TurretDatas.Length > 0;
    }
}