using Sirenix.OdinInspector;
using UnityEngine;

namespace Systems.StateMachine
{
    public abstract class IntervalState<T> : State<T> 
        where T : MonoBehaviour
    {
        [SerializeField,BoxGroup("Interval State")] protected bool _unscaledTime;
        [SerializeField,BoxGroup("Interval State"), Tooltip("seconds between execution method")] protected float _executionInterval;
        [SerializeField,BoxGroup("Interval State"), Tooltip("seconds between state check method")] protected float _stateCheckInterval;
        protected float _executionTimer;
        protected float _stateCheckTimer;

        public override void Enter(T parent)
        {
            _executionTimer = 0;
            _stateCheckTimer = 0;
            base.Enter(parent);
        }

        public override void UpdateState(T parent)
        {
            if (_executionTimer >= _executionInterval)
            {
                IntervalUpdate(parent);
                _executionTimer = 0;
            }
            else
            {
                _executionTimer += _unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            }
        }
        protected abstract void IntervalUpdate(T parent);
        public override void ChangeState(T parent)
        {
            if (_stateCheckTimer >= _stateCheckInterval)
            {
                IntervalChangeState(parent);
                _stateCheckTimer = 0;
            }
            else
            {
                _stateCheckTimer += _unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            }
        }
        protected abstract void IntervalChangeState(T parent);


    }
}