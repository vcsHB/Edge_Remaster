namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorState
    {
        protected GridSelector _selector;
        protected OptionSelector _optionSelector;
        protected SelectorStateMachine _stateMachine;
        protected SelectorMover _mover;

        public SelectorState(GridSelector selector, SelectorStateMachine stateMachine)
        {
            _selector = selector;
            _stateMachine = stateMachine;
            _mover = selector.MoverCompo;
            _optionSelector = selector.OptionSelector;

        }
        public virtual void Enter()
        {

        }

        public virtual void UpdateState()
        {

        }

        public virtual void Exit()
        {

        }
    }
}