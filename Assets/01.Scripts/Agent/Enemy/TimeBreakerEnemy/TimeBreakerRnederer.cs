using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{

    public class TimeBreakerRnederer : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        public UnityEvent OnTimeBreakStartEvent;
        public UnityEvent OnTimeBreakEndEvent;

        public void Initialize(Agent agent) { }
        public void AfterInit() { }

        public void Dispose() { }

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