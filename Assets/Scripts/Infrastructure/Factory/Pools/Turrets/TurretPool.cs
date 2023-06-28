using Services.Factory;
using Services.Wallet;
using TurretLogic;
using TurretLogic.Points;

namespace Infrastructure.Factory.Pools.Turrets
{
    public class TurretPool
    {
        private readonly Turret[] _turrets;
        private readonly TurretData[] _turretDatas;

        public TurretPool(IGameFactory factory, SpawnPointTurret[] spawnPointTurrets, Pool pool, IWallet wallet)
        {
            _turrets = new Turret[spawnPointTurrets.Length];
            
            for (int i = 0; i < _turrets.Length; i++)
            {
                Turret turret = factory.CreateTurret();
                turret.Construct(spawnPointTurrets[i].GetPosition(), wallet);
                spawnPointTurrets[i].Get<TurretPointTrigger>().SetTurret(turret);
                turret.Get<TurretShooting>().Inject(pool);
                turret.gameObject.SetActive(false);
                _turrets[i] = turret;
            }
        }
        
        public Turret[] GetTurrets() =>
            _turrets;
    }
}