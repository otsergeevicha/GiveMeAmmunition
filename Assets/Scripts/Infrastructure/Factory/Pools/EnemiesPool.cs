using System.Linq;
using EnemyLogic;
using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    enum TypeEnemy
    {
        One = 0,
        Two = 1,
        Three = 2
    }
    public class EnemiesPool
    {
        private readonly EnemyOneTypePool _oneTypePool;
        private readonly EnemyTwoTypePool _twoTypePool;
        private readonly EnemyThreeTypePool _threeTypePool;

        public EnemiesPool(IGameFactory factory, string oneTypeEnemy, string twoTypeEnemy, string threeTypeEnemy)
        {
            _oneTypePool = new EnemyOneTypePool(factory, oneTypeEnemy);
            _twoTypePool = new EnemyTwoTypePool(factory, twoTypeEnemy);
            _threeTypePool = new EnemyThreeTypePool(factory, threeTypeEnemy);
        }

        public Enemy TryGet(int typeEnemy)
        {
            return typeEnemy switch
            {
                (int)TypeEnemy.One => 
                    GetOneType(),
                (int)TypeEnemy.Two => 
                    _twoTypePool.Get().FirstOrDefault(enemy => 
                        enemy.isActiveAndEnabled == false),
                (int)TypeEnemy.Three => 
                    _threeTypePool.Get().FirstOrDefault(enemy => 
                        enemy.isActiveAndEnabled == false),
                _ => GetOneType()
            };
        }

        private Enemy GetOneType() =>
            _oneTypePool.Get().FirstOrDefault(enemy =>
                enemy.isActiveAndEnabled == false);
    }
}