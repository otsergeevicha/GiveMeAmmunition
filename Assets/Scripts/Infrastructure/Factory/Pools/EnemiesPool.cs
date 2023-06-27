using EnemyLogic;
using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    public class EnemiesPool
    {
        private readonly Enemy[] _enemies;

        public EnemiesPool(IGameFactory factory, string oneTypeEnemy, string twoTypeEnemy, string threeTypeEnemy)
        {
            _enemies = new Enemy[Constants.AmountEnemy];

            for (int i = 0; i < _enemies.Length; i++)
            {
                Enemy enemy = factory.CreateEnemy(oneTypeEnemy);
                enemy.gameObject.SetActive(false);
                _enemies[i] = enemy;
                
                Enemy enemy1 = factory.CreateEnemy(twoTypeEnemy);
                enemy1.gameObject.SetActive(false);
                _enemies[i+1] = enemy1;
                
                Enemy enemy2 = factory.CreateEnemy(threeTypeEnemy);
                enemy2.gameObject.SetActive(false);
                _enemies[i+2] = enemy2;
            }
        }

        public Enemy[] GetEnemies() =>
            _enemies;
    }
}