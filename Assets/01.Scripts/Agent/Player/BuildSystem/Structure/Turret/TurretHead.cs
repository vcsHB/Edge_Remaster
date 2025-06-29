using UnityEngine;
namespace BuildSystem.Structures.Turrets
{

    public class TurretHead : MonoBehaviour
    {
        [Header("Aiming Setting")]
        [SerializeField] private float _aimingSpeed = 5f; // Head Rotation Speed
        [SerializeField, Range(0f, 180f)] private float _launchAngle = 10f;

       

        public bool SetDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude == 0f)
                return false;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float currentAngle = transform.eulerAngles.z;

            float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);

            float maxStep = _aimingSpeed * Time.fixedDeltaTime;
            float rotateAmount = Mathf.Clamp(angleDiff, -maxStep, maxStep);
            transform.Rotate(0f, 0f, rotateAmount);

            return Mathf.Abs(angleDiff) <= _launchAngle * 0.5f;
        }
    }
}