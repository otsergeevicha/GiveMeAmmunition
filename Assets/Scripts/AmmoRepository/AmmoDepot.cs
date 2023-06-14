namespace AmmoRepository
{
    public class AmmoDepot
    {
        private readonly int _maxSize;
        private int _size;

        public AmmoDepot(int ammoDepotSize)
        {
            _size = ammoDepotSize;
            _maxSize = _size;
        }

        public bool Check() => 
            _size != 0;

        public void Spend(int requiredAmountAmmo)
        {
            _size -= requiredAmountAmmo;

            if (_size < 0) 
                _size = 0;
        }

        public void Replenishment() => 
            _size = _maxSize;
    }
}