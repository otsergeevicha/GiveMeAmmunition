using System;
using System.Collections.Generic;
using EnemyLogic.AI.States;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyLogic.AI
{
    public class EnemyStateMachine
    {
        private Dictionary<Type, ISwitcherState> _states;
        private ISwitcherState _currentState;

        private NavMeshAgent _agent;
        private Transform _target;
        private Animator _animator;
        private int _damage;
        private Transform _transform;
        private State _state;

        public EnemyStateMachine(NavMeshAgent agent, Transform target, Animator animator, int damage,
            Transform transform)
        {
            _transform = transform;
            _damage = damage;
            _target = target;
            _agent = agent;
            _animator = animator;
            
            InitializeStates();
            GeneralDependencyInjections();
            FirstEnterState();
        }

        public void EnterState<TState>() where TState : ISwitcherState
        {
            var state = _states[typeof(TState)];
            _currentState.Disable();
            state.Enable();
            _currentState = state;
        }

        private void InitializeStates()
        {
            _states = new Dictionary<Type, ISwitcherState>
            {
                [typeof(EnemyIdleState)] = new EnemyIdleState(_animator),
                [typeof(EnemyMovementState)] = new EnemyMovementState(_agent, _target, _animator),
                [typeof(EnemyAttackState)] = new EnemyAttackState(_target, _animator, _damage, _transform),
                [typeof(EnemyHitState)] = new EnemyHitState(_animator)
            };
        }

        private void GeneralDependencyInjections()
        {
            foreach (var state in _states)
            {
                state.Value.Init(this);
                state.Value.Disable();
            }
        }

        private void FirstEnterState()
        {
            _currentState = _states[typeof(EnemyIdleState)];
            EnterState<EnemyIdleState>();
        }
    }
}