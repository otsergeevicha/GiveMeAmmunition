using Infrastructure;
using Plugins.MonoCache;
using Services.Health;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoCache, IHealth
    {
        [SerializeField] private Transform _target;
        
        private int _damage;

        private NavMeshAgent _agent;
        private Animator _animator;
        
        private void Start()
        {
            _agent = Get<NavMeshAgent>();
            _animator = Get<Animator>();
            _damage = Constants.EnemyDamage;
        }

        public void TakeDamage(int damage) {}
    }
}