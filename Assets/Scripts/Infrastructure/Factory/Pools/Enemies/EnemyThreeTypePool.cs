﻿using EnemyLogic;
using Services.Factory;
using Services.Factory.Pools;

namespace Infrastructure.Factory.Pools.Enemies
{
    public class EnemyThreeTypePool : IEnemyPool
    {
        private readonly Enemy[] _pool;

        public EnemyThreeTypePool(IGameFactory factory, string threeTypeEnemy)
        {
            _pool = new Enemy[Constants.AmountEnemy];

            for (int i = 0; i < _pool.Length; i++)
            {
                Enemy enemy = factory.CreateEnemy(threeTypeEnemy);
                enemy.gameObject.SetActive(false);
                _pool[i] = enemy;
            }
        }
        
        public Enemy[] Get() =>
            _pool;
    }
}