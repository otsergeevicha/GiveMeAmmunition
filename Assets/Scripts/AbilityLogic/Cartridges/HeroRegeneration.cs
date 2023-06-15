using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure;

namespace AbilityLogic.Cartridges
{
    public class HeroRegeneration
    {
        private readonly CancellationTokenSource _tokenReplenishment = new();
        private readonly IMagazine _magazine;

        public bool IsWaiting;
        
        private bool _isReplenishment;

        public HeroRegeneration(IMagazine magazine) => 
            _magazine = magazine;

        public async void Launch(int delayRegeneration)
        {
            IsWaiting = true;
            await UniTask.Delay(delayRegeneration);
            IsWaiting = false;
            
            Replenishment();
        }
        
        private async void Replenishment()
        {
            _isReplenishment = true;

            while (_isReplenishment)
            {
                _magazine.Replenishment(() => _isReplenishment = false);
                await UniTask.Delay(TimeSpan.FromSeconds(Constants.DelayRegeneration));
            }
        }

        public void StopReplenishment() =>
            _tokenReplenishment.Cancel();
    }
}