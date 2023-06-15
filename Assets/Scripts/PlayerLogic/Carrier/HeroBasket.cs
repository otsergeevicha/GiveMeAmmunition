using System;

namespace PlayerLogic.Carrier
{
    public class HeroBasket
    {
        private readonly int _maxSize;
        private int _size;

        public HeroBasket(int size)
        {
            _size = size;
            _maxSize = size;
        }

        public event Action IsEmpty;

        public int Cartridge =>
            _size;

        public bool IsReplenishmentRequired() =>
            _maxSize != _size;

        public int GetAmmo(int requiredAmountAmmo)
        {
            _size -= requiredAmountAmmo;

            if (_size >= 0) 
                return requiredAmountAmmo;
            
            _size = 0;
            IsEmpty?.Invoke();
            return _size;
        }

        public void ApplyAmmo(int getAmmo, Action fulled)
        {
            _size += getAmmo;

            if (_size >= _maxSize)
            {
                _size = _maxSize;
                fulled?.Invoke();
            }
        }
    }
}