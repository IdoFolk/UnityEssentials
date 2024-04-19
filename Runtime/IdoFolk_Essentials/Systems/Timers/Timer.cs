using System;
using UnityEngine;

namespace IdoFolk_Essentials.Systems.Timers
{
    public class Timer : ITimer
    {
        public event Action<Timer> OnTimerEnd;

        private readonly Action _onTimerTick;
        private readonly float _tickTime;
        private readonly float _tickAmount;
        private readonly bool _usingGameTime;
        private readonly bool _dontDestroyTimer;

        private float _timer;
        private int _currentTicks;
        private bool _isActive;

        public Timer(TimerData timerData)
        {
            _timer = 0;
            _tickTime = timerData.TickTime;
            _tickAmount = timerData.TickAmount;
            _usingGameTime = timerData.UsingGameTime;
            _dontDestroyTimer = timerData.DontDestroyTimer;
            _onTimerTick = timerData.OnTimerTick;
            _isActive = true;
        }

        public void StartTimer() => _isActive = true;
        public void PauseTimer() => _isActive = false;

        public void StopTimer()
        {
            _isActive = false;
            _timer = 0;
        }

        public void TimerTick()
        {
            if (!_isActive) return;

            //update timer
            if (!_usingGameTime) _timer += Time.unscaledDeltaTime;
            else _timer += Time.deltaTime;

            if (_timer >= _tickTime)
            {
                _timer = 0;

                OnTimerTick();
                if (_dontDestroyTimer) return;

                _currentTicks++;
                if (_currentTicks >= _tickAmount)
                {
                    _isActive = false;
                    OnEndTimer();
                }
            }
        }

        protected virtual void OnTimerTick()
        {
            if (_onTimerTick is null) return;
            _onTimerTick?.Invoke();
        }

        protected virtual void OnEndTimer() => OnTimerEnd?.Invoke(this);

        public void RemoveThisTimer()
        {
            TimerManager.Instance.RemoveTimer(this);
        }

        public void AddThisTimer()
        {
            TimerManager.Instance.AddTimer(this);
        }
    }

    public class Timer<T> : ITimer
    {
        public event Action<Timer<T>> OnTimerEnd;

        private readonly Action<T> _onTimerTick;
        private readonly float _tickTime;
        private readonly float _tickAmount;
        private readonly bool _usingGameTime;
        private readonly bool _dontDestroyTimer;

        private float _timer;
        private int _currentTicks;
        private bool _isActive;
        private T _data;

        public T Data
        {
            get => _data;
        }

        public Timer(TimerData<T> timerData)
        {
            _timer = 0;
            _data = timerData.Data;
            _tickTime = timerData.TickTime;
            _tickAmount = timerData.TickAmount;
            _usingGameTime = timerData.UsingGameTime;
            _dontDestroyTimer = timerData.DontDestroyTimer;
            _onTimerTick = timerData.OnTimerTick;
            _isActive = true;
        }

        public void StartTimer() => _isActive = true;
        public void PauseTimer() => _isActive = false;

        public void StopTimer()
        {
            _isActive = false;
            _timer = 0;
        }

        public void TimerTick()
        {
            if (!_isActive) return;

            //update timer
            if (!_usingGameTime) _timer += Time.unscaledDeltaTime;
            else _timer += Time.deltaTime;

            if (_timer >= _tickTime)
            {
                _timer = 0;

                OnTimerTick();
                if (_dontDestroyTimer) return;

                _currentTicks++;
                if (_currentTicks >= _tickAmount)
                {
                    _isActive = false;
                    OnEndTimer();
                }
            }
        }

        protected virtual void OnTimerTick()
        {
            if (_onTimerTick is null) return;
            _onTimerTick?.Invoke(_data);
        }

        protected virtual void OnEndTimer() => OnTimerEnd?.Invoke(this);

        public void RemoveThisTimer()
        {
            TimerManager.Instance.RemoveTimer<T>(this);
        }

        public void AddThisTimer()
        {
            TimerManager.Instance.AddTimer<T>(this);
        }
    }

    public struct TimerData
    {
        public readonly Action OnTimerTick;
        public readonly float TickTime;
        public readonly float TickAmount;
        public readonly bool UsingGameTime;
        public readonly bool DontDestroyTimer;

        public TimerData(float tickTime, Action onTimerTick = null, float tickAmount = 1, bool usingGameTime = false,
            bool dontDestroyTimer = false)
        {
            OnTimerTick = onTimerTick;
            TickTime = tickTime;
            TickAmount = tickAmount;
            UsingGameTime = usingGameTime;
            DontDestroyTimer = dontDestroyTimer;
        }
    }

    public struct TimerData<T>
    {
        public readonly Action<T> OnTimerTick;
        public readonly float TickTime;
        public readonly float TickAmount;
        public readonly bool UsingGameTime;
        public readonly bool DontDestroyTimer;
        public readonly T Data;


        public TimerData(float tickTime, T data, Action<T> onTimerTick = null, float tickAmount = 1,
            bool usingGameTime = false, bool dontDestroyTimer = false)
        {
            Data = data;
            OnTimerTick = onTimerTick;
            TickTime = tickTime;
            TickAmount = tickAmount;
            UsingGameTime = usingGameTime;
            DontDestroyTimer = dontDestroyTimer;
        }
    }
}