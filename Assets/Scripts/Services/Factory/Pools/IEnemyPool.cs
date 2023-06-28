using EnemyLogic;

namespace Services.Factory.Pools
{
    public interface IEnemyPool
    {
        Enemy[] Get();
    }
}