using EnemyLogic;

namespace Infrastructure.Factory.Pools
{
    public interface IEnemyPool
    {
        Enemy[] Get();
    }
}