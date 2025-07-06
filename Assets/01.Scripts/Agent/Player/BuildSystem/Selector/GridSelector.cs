using BuildSystem.ResourceManage.UI;
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
        [field: SerializeField] public BuildController BuildController { get; private set; }
        public StructureInfoPanel InfoPanel { get; private set; }
        public RequireResourcePanel RequireResourcePanel { get; private set;}


        [Header("Detect Setting")]

        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private LayerMask _structureLayer;
        [SerializeField] private Vector2 _detectArea;


        private SelectorStateMachine _stateMachine;
        public Structure CurrentSelectedStructure { get; private set; }

        private void Awake()
        {
            MoverCompo = GetComponent<SelectorMover>();
            OptionSelector = GetComponentInChildren<OptionSelector>();
            InfoPanel = GetComponentInChildren<StructureInfoPanel>();
            RequireResourcePanel = GetComponentInChildren<RequireResourcePanel>();

            _stateMachine = new SelectorStateMachine(this);
            _stateMachine.Initialize(SelectorStateEnum.Stay);
        }

        private void Update()
        {
            _stateMachine.UpdateCurrentState();
        }

        public bool DetectObstacle()
        {
            return Physics2D.OverlapBox(transform.position, _detectArea, 0f, _obstacleLayer) != null;
        }

        public Structure DetectStructure()
        {
            Collider2D target = Physics2D.OverlapBox(transform.position, _detectArea, 0f, _structureLayer);
            if (target == null) return null;
            return target.GetComponent<Structure>();
        }

        public void SetStructure(Structure structure)
        {
            CurrentSelectedStructure = structure;
        }
    }
}