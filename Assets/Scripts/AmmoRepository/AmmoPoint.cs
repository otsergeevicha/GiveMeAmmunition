using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Plugins.MonoCache;

namespace AmmoRepository
{
    enum LevelAmmoPoint
    {
        One = 0,
        Two = 1,
        Three = 2,
        Four = 3
    }
    public class AmmoPoint : MonoCache
    {
        private readonly CancellationTokenSource _tokenAirDrop = new ();
        private AmmoDepot _depot;

        private void Awake() => 
            _depot = new AmmoDepot(Constants.AmmoDepotSize);

        public int GetAmmo()
        {
            if (_depot.Check())
            {
                _depot.Spend(Constants.AmountAmmo);
                return Constants.AmountAmmo;
            }
            else
            {
                CallAirDrop();
                return 0;
            }
        }

        private async void CallAirDrop()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(Constants.TimeSpawnAirDrop));
            _depot.Replenishment();
            _tokenAirDrop.Cancel();
        }
    }
}