using BuildSystem.SelectorManage.FSM;
using InputManage;
using UnityEngine;
namespace BuildSystem.SelectorManage
{

    public class GridSelector : MonoBehaviour
    {
        [field: SerializeField] public PlayerInput SelectorInput { get; private set; }
        public SelectorMover MoverCompo { get; private set; }

        private SelectorStateMachine _stateMachine;

        private void Awake()
        {
            MoverCompo = GetComponent<SelectorMover>();
            
            _stateMachine = new SelectorStateMachine(this);
            _stateMachine.Initialize(SelectorStateEnum.Stay);
        }

        private void Update()
        {
            _stateMachine.UpdateCurrentState();
        }
    }
}