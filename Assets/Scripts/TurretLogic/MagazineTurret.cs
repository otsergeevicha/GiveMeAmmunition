using System;

namespace TurretLogic
{
    public class MagazineTurret
    {
        private readonly int _maxSize;
        private int _size;

        public MagazineTurret(int size)
        {
            _size = size;
            _maxSize = size;
        }

        public void Spend() => 
            _size--;

        public bool Check() => 
            _size != 0;

        public void ApplyAmmo(int newAmmo, Action fulled)
        {
            _size += newAmmo;

            if (_size >= _maxSize)
            {
                _size = _maxSize;
                fulled?.Invoke();
            }
        }
    }
}