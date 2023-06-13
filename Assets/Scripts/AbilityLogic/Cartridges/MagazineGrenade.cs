namespace AbilityLogic.Cartridges
{
    public class MagazineGrenade
    {
        private int _size;

        public MagazineGrenade(int size) => 
            _size = size;

        public void Spend() => 
            _size--;

        public bool Check() => 
            _size != 0;
    }
}