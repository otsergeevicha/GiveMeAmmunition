using System;
using TurretLogic;

namespace Infrastructure.SaveLoadLogic
{
    [Serializable]
    public class DataTurretPool
    {
        public DataTurretPool() => 
            Turrets = Array.Empty<Turret>();

        public Turret[] Turrets { get; private set; }

        public void Record(Turret[] turrets) =>
            Turrets = turrets;
    }
}