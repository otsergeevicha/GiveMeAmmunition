namespace WaveLogic
{
    public class DataWave
    {
        public void InjectDependency(int currentLevel, float timeLevel, 
            string oneTypeEnemy, string twoTypeEnemy, string threeTypeEnemy)
        {
            ThreeTypeEnemy = threeTypeEnemy;
            TwoTypeEnemy = twoTypeEnemy;
            OneTypeEnemy = oneTypeEnemy;
            
            TimeLevel = timeLevel;
            CurrentLevel = currentLevel;
        }

        public float TimeLevel { get; private set; }
        public int CurrentLevel { get; private set; }
        public string OneTypeEnemy { get; private set; }
        public string TwoTypeEnemy { get; private set; }
        public string ThreeTypeEnemy { get; private set; }
    }
}