using Services.Factory;
using TurretLogic;

namespace Ammo.Pools
{
    public class TurretPool
    {
        private readonly Turret[] _turrets;

        public TurretPool(IGameFactory factory, SpawnPointTurret[] spawnPointTurrets)
        {
            _turrets = new Turret[spawnPointTurrets.Length];

            for (int i = 0; i < _turrets.Length; i++)
            {
                Turret turret = factory.CreateTurret();
                turret.SetPosition(spawnPointTurrets[i].GetPosition());
                turret.gameObject.SetActive(false);
                _turrets[i] = turret;
            }
        }

        public Turret[] GetTurrets() =>
            _turrets;
    }
}