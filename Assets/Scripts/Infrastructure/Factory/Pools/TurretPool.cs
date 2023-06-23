using Services.Factory;
using Services.SaveLoadLogic;
using Services.Wallet;
using TurretLogic;
using TurretLogic.Points;

namespace Infrastructure.Factory.Pools
{
    public class TurretPool
    {
        private Turret[] _turrets;

        public void InjectDependence(IGameFactory factory, SpawnPointTurret[] spawnPointTurrets, Pool pool, IWallet wallet, ISave save)
        {
            _turrets = new Turret[spawnPointTurrets.Length];

            for (int i = 0; i < _turrets.Length; i++)
            {
                Turret turret = factory.CreateTurret();
                turret.Construct(spawnPointTurrets[i].GetPosition(), wallet, save);
                spawnPointTurrets[i].SetTurret(turret);
                turret.Get<TurretShooting>().Inject(pool);
                turret.gameObject.SetActive(turret.Purchased);
                _turrets[i] = turret;
            }
        }

        public Turret[] GetTurrets() =>
            _turrets;
    }
}