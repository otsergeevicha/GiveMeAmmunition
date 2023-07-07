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

        private bool _isWaiting;
        private bool _isReplenishment;

        public HeroRegeneration(IMagazine magazine) => 
            _magazine = magazine;

        public bool IsWaiting =>
            _isWaiting;
        
        public async UniTaskVoid Launch(int delayRegeneration)
        {
            _isWaiting = true;
            await UniTask.Delay(delayRegeneration);
            _isWaiting = false;
            
            _ = Replenishment();
        }
        
        private async UniTaskVoid Replenishment()
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