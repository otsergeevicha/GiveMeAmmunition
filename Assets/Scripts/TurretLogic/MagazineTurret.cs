namespace TurretLogic
{
    public class MagazineTurret
    {
        private int _size;

        public MagazineTurret(int size) => 
            _size = size;

        public void Spend() => 
            _size--;

        public bool Check() => 
            _size != 0;
    }
}