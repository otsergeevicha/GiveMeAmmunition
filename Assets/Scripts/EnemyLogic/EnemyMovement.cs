using Infrastructure;
using Plugins.MonoCache;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BotInput))]
    public class EnemyMovement : MonoCache
    {
        private IBotInput _inputSource;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _inputSource = Get<BotInput>();
            _agent = Get<NavMeshAgent>();
            
            _agent.speed = Constants.EnemySpeed;
        }

        public void Move() => 
            _agent.SetDestination(new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y));
    }
}