using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdoFolk_Essentials.Systems.StateMachine
{
    public abstract class BaseStateMachine<T> : MonoBehaviour 
        where T : MonoBehaviour
    {
        public State<T> ActiveState => _activeState;

        public Dictionary<Type, State<T>> States => _states;

        protected State<T> _activeState;
        
        private Dictionary<Type, State<T>> _states = new ();
        
        private bool _isActive;
        
        protected void BaseInit(List<State<T>> states)
        {
            _states = new ();
            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }

            _activeState = states[0];
            _isActive = true;
        }

        public void SetState(Type newStateType)
        {
            if (_activeState != null)
            {
                _activeState.Exit(this as T);
            }

            _activeState = _states[newStateType];
            _activeState.Enter(this as T);
        }

        public void Resume() => _isActive = true;
        public void Stop() => _isActive = false;

        private void Update()
        {
            if (!_isActive) return;
            _activeState.UpdateState(this as T);
            _activeState.ChangeState(this as T);
        }
    }
    
}