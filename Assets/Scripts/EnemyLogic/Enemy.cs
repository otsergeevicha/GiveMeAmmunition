using EnemyLogic.AI;
using EnemyLogic.AI.States;
using PlayerLogic;
using Plugins.MonoCache;
using TurretLogic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoCache, IHealth
    {
        [SerializeField] private Transform _target;
        [SerializeField] private int _damage;

        private NavMeshAgent _agent;
        private Animator _animator;
        private EnemyStateMachine _stateMachine;

        private State _state;
        
        private void Start()
        {
            _agent = Get<NavMeshAgent>();
            _animator = Get<Animator>();
            _stateMachine = new EnemyStateMachine(_agent, _target, _animator, _damage, transform);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Hero hero))
            {
                _target = hero.transform;
                _stateMachine.EnterState<EnemyAttackState>();
            }
            
            if (collision.gameObject.TryGetComponent(out Turret turret))
            {
                _target = turret.transform;
                _stateMachine.EnterState<EnemyAttackState>();
            }
            
            if (collision.gameObject.TryGetComponent(out Bullet _)) 
                _stateMachine.EnterState<EnemyHitState>();
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}