using Plugins.MonoCache;
using Services.Health;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoCache, IHealth
    {
        private Vector3 _currentTarget;

        public void TakeDamage(int damage)
        {
        }

        public void InjectTarget(Vector3 newTarget) => 
            _currentTarget = newTarget;
    }
}