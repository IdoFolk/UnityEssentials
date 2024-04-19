namespace IdoFolk_Essentials.Systems.Timers
{
    public interface ITimer
    {
        public void StartTimer();
        public void PauseTimer();
        public void StopTimer();
        public void TimerTick();

        public void RemoveThisTimer();

        public void AddThisTimer();
    }
}