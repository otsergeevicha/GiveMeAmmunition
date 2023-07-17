using BehaviorDesigner.Runtime;
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
        public void TakeDamage(int damage) {}
    }
}