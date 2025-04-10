using InputManage;
using UnityEngine;
namespace Agents.Players.Combat
{


    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        private SpriteRenderer _spriteRenderer;

        public Vector2 Position => _playerInput.MousePosition;

        private void Awake() {
            
            _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }

        public void SetEnabled(bool value) => _spriteRenderer.enabled = value;

        private void Update() {
            
            transform.position = _playerInput.MousePosition;
        }
    }
}