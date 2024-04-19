using System;
using UnityEngine;

namespace IdoFolk_Essentials.Systems.StateMachine
{
    public abstract class State<T> : ScriptableObject 
        where T : MonoBehaviour 
    {
        public event Action<State<T>> StateEnter;
        public event Action<State<T>> StateExit;
        public virtual void Enter(T parent) => StateEnter?.Invoke(this);
        public abstract void UpdateState(T parent);
        public abstract void ChangeState(T parent);
        public virtual void Exit(T parent) => StateExit?.Invoke(this);
    }
}