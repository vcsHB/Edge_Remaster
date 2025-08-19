using UnityEngine;
using UnityEngine.Events;
namespace TitleScene
{

    public class MoveDetector : MonoBehaviour
    {
        public UnityEvent OnEnterEvent;
        [Header("Detect Setting")]
        [SerializeField] private Vector2 _detectDirection;
        [SerializeField] private float _dotThreshold = 0.8f;
        private Vector2 _prevPos;
        private bool _isInside;
        private bool _isInvoked;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _prevPos = other.transform.position;
            _isInside = true;
            _isInvoked = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_isInside) return;

            Vector2 current = other.transform.position;
            Vector2 moveDir = (current - _prevPos).normalized;

            if (Vector2.Dot(moveDir, _detectDirection) > _dotThreshold && !_isInvoked)
            {
                _isInvoked = true;
                OnEnterEvent?.Invoke();
            }

            _prevPos = current;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isInside = false;
        }
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (Mathf.Approximately(_detectDirection.magnitude, 0f)) return;
            Vector3 start = transform.position;

            Vector3 dir = ((Vector3)_detectDirection).normalized;
            float arrowLength = 1.0f; // Arrow Gizmos Length
            Vector3 end = start + dir * arrowLength;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);

            DrawArrowHead(end, dir, 0.25f, 20f);
        }

        private void DrawArrowHead(Vector3 position, Vector3 direction, float size, float angle)
        {
            Vector3 right = Quaternion.LookRotation(Vector3.forward, direction) *
                            Quaternion.Euler(0, 0, angle) * Vector3.down;
            Vector3 left = Quaternion.LookRotation(Vector3.forward, direction) *
                            Quaternion.Euler(0, 0, -angle) * Vector3.down;

            Gizmos.DrawLine(position, position + right * size);
            Gizmos.DrawLine(position, position + left * size);
        }
#endif
    }
}