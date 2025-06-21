using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace BuildSystem.SelectorManage
{

    public class SelectorMover : MonoBehaviour
    {
        public event Action OnArriveEvent;
        [SerializeField] private float _selectMoveDuration = 0.07f;
        private bool _isMoving;
        private bool _canMove = true;
        public Vector2Int SelectPosition { get; private set; }

        public void SetCanMove(bool value)
        {
            _canMove = value;
        }

        public void HandleMove(Vector2 inputDirection)
        {
            if (_isMoving || !_canMove) return;
            int x = Mathf.RoundToInt(inputDirection.x);
            int y = Mathf.RoundToInt(inputDirection.y);

            int newX = SelectPosition.x + x;
            int newY = SelectPosition.y + y;

            if (newX % 2 == 0) newX += (x > 0) ? 1 : -1;
            if (newY % 2 == 0) newY += (y > 0) ? 1 : -1;

            SelectPosition = new Vector2Int(newX, newY);
            _isMoving = true;
            transform.DOMove((Vector2)SelectPosition, _selectMoveDuration).OnComplete(HandleMoveArrive);
        }
        private void HandleMoveArrive()
        {
            _isMoving = false;
            OnArriveEvent?.Invoke();
        }

    }
}