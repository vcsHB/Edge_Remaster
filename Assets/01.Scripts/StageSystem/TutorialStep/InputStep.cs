using InputManage;
using UnityEngine;
namespace StageSystem.TutorialManage
{
    public enum InputType
    {
        Enter,
        C,
        X,
        F

    }
    public class InputStep : TutorialStep
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private InputType _inputType;
        public override void Enter()
        {
            base.Enter();
            switch (_inputType)
            {
                case InputType.Enter:
                    _playerInput.OnSelectEvent += Exit;
                    break;

                case InputType.C:
                    _playerInput.OnCancelEvent += Exit;
                    break;
                case InputType.X:
                    _playerInput.OnBuildDestroyEvent += Exit;
                    break;
                case InputType.F:
                    _playerInput.OnInteractEvent += Exit;
                    break;
            }
        }

        public override void Exit()
        {
            base.Exit();
            switch (_inputType)
            {
                case InputType.Enter:
                    _playerInput.OnSelectEvent -= Exit;
                    break;

                case InputType.C:
                    _playerInput.OnCancelEvent -= Exit;
                    break;
                case InputType.X:
                    _playerInput.OnBuildDestroyEvent -= Exit;
                    break;
                case InputType.F:
                    _playerInput.OnInteractEvent -= Exit;
                    break;
            }
        }
    }
}