using BehaviorDesigner.Runtime;
using Infrastructure;
using Plugins.MonoCache;
using Services.Health;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    [RequireComponent(typeof(BehaviorTree))]
    [RequireComponent(typeof(BotInput))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoCache, IHealth
    {
        private EnemyHealth _health;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _health = new EnemyHealth(Constants.EnemyMaxHealth);
            _agent = Get<NavMeshAgent>();
        }

        protected override void OnEnabled() => 
            _health.Died += OnReset;

        protected override void OnDisabled() => 
            _health.Died -= OnReset;

        public void TakeDamage(int damage) => 
            _health.ApplyDamage(damage);

        private void OnReset()
        {
            _agent.isStopped = true;
            gameObject.SetActive(false);
            _health.ReturnMaxHealth();
        }
    }
}