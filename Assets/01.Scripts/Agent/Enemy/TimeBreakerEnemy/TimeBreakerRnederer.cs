using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{

    public class TimeBreakerRnederer : EnemyRenderer
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        public UnityEvent OnTimeBreakStartEvent;
        public UnityEvent OnTimeBreakEndEvent;

        public void StartTimeBreak()
        {
            OnTimeBreakStartEvent?.Invoke();
        }

        public void EndTimeBreak()
        {
            OnTimeBreakEndEvent?.Invoke();
        }

    }
}