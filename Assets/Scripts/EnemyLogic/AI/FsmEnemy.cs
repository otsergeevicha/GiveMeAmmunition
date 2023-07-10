using System;
using System.Collections.Generic;
using EnemyLogic.AI.States;
using Plugins.MonoCache;
using UnityEngine;

namespace EnemyLogic.AI
{
    [RequireComponent(typeof(SearchTargetState))]
    [RequireComponent(typeof(MovementState))]
    public class FsmEnemy : MonoCache
    {
        private Dictionary<Type, ISwitcherState> _allStates;
        private ISwitcherState _currentState;

        private void Start()
        {
            _allStates = new Dictionary<Type, ISwitcherState>
            {
                [typeof(SearchTargetState)] = Get<SearchTargetState>(),
                [typeof(MovementState)] = Get<MovementState>()
            };

            foreach (var state in _allStates)
            {
                state.Value.Init(this);
                state.Value.Exit();
            }
            
            _currentState = _allStates[typeof(SearchTargetState)];
            Enter<SearchTargetState>();
        }

        public void Enter<TState>() where TState : ISwitcherState
        {
            var state = _allStates[typeof(TState)];
            _currentState.Exit();
            state.Enter();
            _currentState = state;
        }
    }
}