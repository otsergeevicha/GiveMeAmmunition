namespace WaveLogic
{
    public class DataWave
    {
        private int _currentLevel;
        private float _timeLevel;
        private string _oneTypeEnemy;
        private string _twoTypeEnemy;
        private string _threeTypeEnemy;

        public void InjectDependency(int currentLevel, float timeLevel,
            string oneTypeEnemy, string twoTypeEnemy, string threeTypeEnemy)
        {
            _threeTypeEnemy = threeTypeEnemy;
            _twoTypeEnemy = twoTypeEnemy;
            _oneTypeEnemy = oneTypeEnemy;
            _timeLevel = timeLevel;
            _currentLevel = currentLevel;
        }

        public float TimeLevel() => 
            _timeLevel;
        public int CurrentLevel() => 
            _currentLevel;
        public string OneTypeEnemy() => 
            _oneTypeEnemy;
        public string TwoTypeEnemy() => 
            _twoTypeEnemy;
        public string ThreeTypeEnemy() => 
            _threeTypeEnemy;
    }
}