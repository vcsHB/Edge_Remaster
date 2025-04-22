using UnityEngine;
namespace Agents.Enemies
{

    public class EnemyMovement : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private float _moveSpeed;
        protected Rigidbody2D _rigid;
        protected Vector2 _moveDirection; // normalized

        public void Initialize(Agent agent) { }
        public void AfterInit() { }
        public void Dispose() { }


        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        public void SetMovement(Vector2 direction)
        {
            _moveDirection = direction.normalized;
            _rigid.linearVelocity = _moveDirection * _moveSpeed;
        }

        public void SetMovement(Vector2 direction, float magnification)
        {
             _moveDirection = direction.normalized;
            _rigid.linearVelocity = _moveDirection * _moveSpeed * magnification;
        }

        public void StopImmediately()
        {
            _rigid.linearVelocity = Vector2.zero;
        }
    }
}