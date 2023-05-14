using EnemyLogic.AI;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private NavMeshAgent _agent;
        private EnemyStateMachine _stateMachine;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _stateMachine = new EnemyStateMachine(_agent, _target);
        }
    }
}