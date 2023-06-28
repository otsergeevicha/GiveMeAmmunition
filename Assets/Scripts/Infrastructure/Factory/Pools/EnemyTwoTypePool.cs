using EnemyLogic;
using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    public class EnemyTwoTypePool : IEnemyPool
    {
        private readonly Enemy[] _pool;
        
        public EnemyTwoTypePool(IGameFactory factory, string twoTypeEnemy)
        {
            _pool = new Enemy[Constants.AmountEnemy];

            for (int i = 0; i < _pool.Length; i++)
            {
                Enemy enemy = factory.CreateEnemy(twoTypeEnemy);
                enemy.gameObject.SetActive(false);
                _pool[i] = enemy;
            }
        }
        
        public Enemy[] Get() =>
            _pool;
    }
}