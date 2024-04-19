using System;
using System.Collections.Generic;
using IdoFolk_Essentials.Helpers;

namespace IdoFolk_Essentials.Systems.Timers
{
    public class TimerManager : MonoSingleton<TimerManager>
    {
        private readonly List<ITimer> _timers = new List<ITimer>();
        private readonly List<ITimer> _queuedTimersToAdd = new List<ITimer>();
        private readonly List<ITimer> _queuedTimersToRemove = new List<ITimer>();

        private void Update()
        {
            _timers.ForEach(timer => timer.TimerTick());

            if (_queuedTimersToAdd.Count > 0) AddQueuedTimers();
            if (_queuedTimersToRemove.Count > 0) RemoveQueuedTimers();
        }

        private void AddQueuedTimers()
        {
            _queuedTimersToAdd.ForEach(queuedTimer => _timers.Add(queuedTimer));
            _queuedTimersToAdd.Clear();
        }

        private void RemoveQueuedTimers()
        {
            _queuedTimersToRemove.ForEach(queuedTimer => _timers.Remove(queuedTimer));
            _queuedTimersToAdd.Clear();
        }

        #region Add Timers Overrides

        /// <summary>
        /// </summary>
        /// <param name="dontDestroyTimer">if true use the RemoveTimer Method</param>
        /// <returns></returns>
        public Timer AddTimer(float tickTime, Action onTimerTick, bool usingGameTime = false, int ticksAmount = 1,
            bool dontDestroyTimer = false)
        {
            var timerData = new TimerData(tickTime, onTimerTick, ticksAmount, usingGameTime, dontDestroyTimer);
            var timer = new Timer(timerData);
            _queuedTimersToAdd.Add(timer);
            timer.OnTimerEnd += RemoveTimer;
            return timer;
        }

        public Timer<T> AddTimer<T>(float tickTime, T data, Action<T> onTimerTick, bool usingGameTime = false,
            int ticksAmount = 1, bool dontDestroyTimer = false)
        {
            var timerData = new TimerData<T>(tickTime, data, onTimerTick, ticksAmount, usingGameTime, dontDestroyTimer);
            var timer = new Timer<T>(timerData);
            _queuedTimersToAdd.Add(timer);
            timer.OnTimerEnd += RemoveTimer;
            return timer;
        }

        public Timer AddTimer(Timer timer)
        {
            _queuedTimersToAdd.Add(timer);
            timer.OnTimerEnd += RemoveTimer;
            return timer;
        }

        public Timer<T> AddTimer<T>(Timer<T> timer)
        {
            _queuedTimersToAdd.Add(timer);
            timer.OnTimerEnd += RemoveTimer;
            return timer;
        }

        #endregion


        /// <summary>
        /// use this Function if the timer is set to dont destroy
        /// </summary>
        /// <param name="timer"></param>
        public void RemoveTimer(Timer timer)
        {
            timer.OnTimerEnd -= RemoveTimer;
            _queuedTimersToRemove.Add(timer);
        }

        public void RemoveTimer<T>(Timer<T> timer)
        {
            timer.OnTimerEnd -= RemoveTimer;
            _queuedTimersToRemove.Add(timer);
        }
    }
}