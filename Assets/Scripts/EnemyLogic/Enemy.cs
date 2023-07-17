using BehaviorDesigner.Runtime;
using Plugins.MonoCache;
using Services.Health;
using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(BehaviorTree))]
    public class Enemy : MonoCache, IHealth
    {
        public void TakeDamage(int damage) {}
    }
}