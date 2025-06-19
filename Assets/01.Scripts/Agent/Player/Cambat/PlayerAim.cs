using System;
using DG.Tweening;
using InputManage;
using UnityEngine;
namespace Agents.Players.Combat
{


    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _selectMoveDuration = 0.07f;
        private bool _isMoving;
        private SpriteRenderer _spriteRenderer;

        public Vector2 Position => _playerInput.MousePosition;
        public Vector2Int SelectPosition { get; private set; }

        private void Awake()
        {

            _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
            _playerInput.OnSelectMoveEvent += HandleSelectMove;
        }
        private void HandleSelectMove(Vector2 inputDirection)
        {
            if (_isMoving) return;
            int x = Mathf.RoundToInt(inputDirection.x);
            int y = Mathf.RoundToInt(inputDirection.y);

            int newX = SelectPosition.x + x;
            int newY = SelectPosition.y + y;

            if (newX % 2 == 0) newX += (x > 0) ? 1 : -1;
            if (newY % 2 == 0) newY += (y > 0) ? 1 : -1;

            SelectPosition = new Vector2Int(newX, newY);
            _isMoving = true;
            transform.DOMove((Vector2)SelectPosition, _selectMoveDuration).OnComplete(() => _isMoving = false);
        }
        public void SetEnabled(bool value) => _spriteRenderer.enabled = value;

        private void Update()
        {

            //transform.position = (Vector2)_playerInput.SelectPosition;
        }
    }
}