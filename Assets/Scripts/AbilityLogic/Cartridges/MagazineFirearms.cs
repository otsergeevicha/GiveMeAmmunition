namespace AbilityLogic.Cartridges
{
    public class MagazineFirearms
    {
        private int _size;

        public MagazineFirearms(int size) => 
            _size = size;

        public void Spend() => 
            _size--;

        public bool Check() => 
            _size != 0;
    }
}