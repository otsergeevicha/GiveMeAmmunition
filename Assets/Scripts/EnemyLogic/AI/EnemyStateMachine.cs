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

        public EnemyStateMachine(NavMeshAgent agent, Transform target)
        {
            _target = target;
            _agent = agent;
            
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
                [typeof(MovementState)] = new MovementState(_agent, _target),
                [typeof(IdleState)] = new IdleState(),
                [typeof(AttackState)] = new AttackState()
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
            _currentState = _states[typeof(IdleState)];
            EnterState<IdleState>();
        }
    }
}