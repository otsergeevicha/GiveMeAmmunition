﻿using EnemyLogic;
using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    public class EnemyOneTypePool : IEnemyPool
    {
        private readonly Enemy[] _pool;
        
        public EnemyOneTypePool(IGameFactory factory, string oneTypeEnemy)
        {
            _pool = new Enemy[Constants.AmountEnemy];

            for (int i = 0; i < _pool.Length; i++)
            {
                Enemy enemy = factory.CreateEnemy(oneTypeEnemy);
                enemy.gameObject.SetActive(false);
                _pool[i] = enemy;
            }
        }

        public Enemy[] Get() =>
            _pool;
    }
}