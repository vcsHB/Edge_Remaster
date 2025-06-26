using BuildSystem.SelectorManage.FSM;
using BuildSystem.Structures;
using BuildSystem.UIManage;
using InputManage;
using UnityEngine;
namespace BuildSystem.SelectorManage
{

    public class GridSelector : MonoBehaviour
    {
        [field: SerializeField] public PlayerInput SelectorInput { get; private set; }
        public SelectorMover MoverCompo { get; private set; }
        public OptionSelector OptionSelector { get; private set; }
        public StructureInfoPanel InfoPanel { get; private set; }
        [Header("Detect Setting")]

        [SerializeField] private LayerMask _structureLayer;
        [SerializeField] private Vector2 _detectArea;


        private SelectorStateMachine _stateMachine;

        private void Awake()
        {
            MoverCompo = GetComponent<SelectorMover>();
            OptionSelector = GetComponentInChildren<OptionSelector>();
            InfoPanel = GetComponentInChildren<StructureInfoPanel>();

            _stateMachine = new SelectorStateMachine(this);
            _stateMachine.Initialize(SelectorStateEnum.Stay);
        }

        private void Update()
        {
            _stateMachine.UpdateCurrentState();
        }

        public Structure DetectStructure()
        {
            Collider2D target = Physics2D.OverlapBox(transform.position, _detectArea, 0f, _structureLayer);
            if (target == null) return null;
            return target.GetComponent<Structure>();
        }
    }
}