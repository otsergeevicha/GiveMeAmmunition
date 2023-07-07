using System;
using Infrastructure;

namespace AbilityLogic.Cartridges
{
    public class MagazineGrenade : IMagazine
    {
        private readonly HeroRegeneration _regeneration;
        private readonly int _maxSize;
        private int _size;

        public MagazineGrenade(int size)
        {
            _size = size;
            _maxSize = size;
            _regeneration = new HeroRegeneration(this);
        }

        public void Spend() => 
            _size--;

        public bool Check() => 
            _size != 0;

        public void Replenishment(Action fulled)
        {
            _size++;
            
            if (_size >= _maxSize) 
                fulled?.Invoke();
        }

        public void Shortage()
        {
            if (_regeneration.IsWaiting)
                return;

            if (_size < _maxSize)
            {
                _regeneration.StopReplenishment();
                _ = _regeneration.Launch(Constants.DelayRegenerationMagazine);
            }
        }
    }
}