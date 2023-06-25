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
        private readonly TurretData[] _turretDatas;
        private readonly ISave _saveLoadService;

        public TurretPool(IGameFactory factory, SpawnPointTurret[] spawnPointTurrets, Pool pool, IWallet wallet,
            ISave saveLoadService)
        {
            _saveLoadService = saveLoadService;
            
            _turrets = new Turret[spawnPointTurrets.Length];
            _turretDatas = GetTurretData(saveLoadService);
            
            for (int i = 0; i < _turrets.Length; i++)
            {
                Turret turret = factory.CreateTurret();
                turret.Construct(spawnPointTurrets[i].GetPosition(), wallet);
                spawnPointTurrets[i].Get<TurretPointTrigger>().SetTurret(turret);
                turret.Get<TurretShooting>().Inject(pool);
                turret.gameObject.SetActive(_turretDatas[i].IsPurchase);
                turret.SelectorTurret(_turretDatas[i].CurrentLevel);
                _turrets[i] = turret;
            }
        }

        public void CurrentSave()
        {
            for (int i = 0; i < _turrets.Length; i++)
            {
                _turretDatas[i].IsPurchase = _turrets[i].Purchased;
                _turretDatas[i].CurrentLevel = _turrets[i].Level;
            }

            _saveLoadService.AccessProgress().DataTurretPool.Record(_turretDatas);
            _saveLoadService.Save();
        }

        public Turret[] GetTurrets() =>
            _turrets;

        private TurretData[] GetTurretData(ISave saveLoadService) => 
            saveLoadService.AccessProgress().DataTurretPool.Check() 
                ? saveLoadService.AccessProgress().DataTurretPool.Read() 
                : new TurretData[_turrets.Length];
    }
}