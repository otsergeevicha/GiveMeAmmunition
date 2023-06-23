using Services.Factory;
using Services.SaveLoadLogic;
using Services.Wallet;
using TurretLogic;
using TurretLogic.Points;

namespace Infrastructure.Factory.Pools
{
    public class TurretPool
    {
        private readonly Turret[] _turrets;

        public TurretPool(IGameFactory factory, SpawnPointTurret[] spawnPointTurrets, Pool pool, IWallet wallet, ISave saveLoadService)
        {
            if (saveLoadService.Progress.DataTurretPool.Turrets.Length > 0)
            {
                _turrets = saveLoadService.Progress.DataTurretPool.Turrets;
            }
            else
            {
                _turrets = new Turret[spawnPointTurrets.Length];
            }

            for (int i = 0; i < _turrets.Length; i++)
            {
                Turret turret = factory.CreateTurret();
                turret.Construct(spawnPointTurrets[i].GetPosition(), wallet);
                spawnPointTurrets[i].SetTurret(turret);
                turret.Get<TurretShooting>().Inject(pool);
                turret.gameObject.SetActive(turret.Purchased);
                _turrets[i] = turret;
            }

            saveLoadService.Progress.DataTurretPool.Record(_turrets);
            saveLoadService.Save();
        }

        public Turret[] GetTurrets() =>
            _turrets;
    }
}