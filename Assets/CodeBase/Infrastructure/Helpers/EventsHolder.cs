using System;
using CodeBase.Infrastructure.Logic;

namespace CodeBase.Infrastructure.Helpers
{
    public sealed class EventsHolder : Singleton<EventsHolder>
    {
        public event Action EnemyDie;
        public event Action PlayerDie;
        public void OnEnemyDie() => 
            EnemyDie?.Invoke();

        public void OnPlayerDie() => 
            PlayerDie?.Invoke();
    }
}