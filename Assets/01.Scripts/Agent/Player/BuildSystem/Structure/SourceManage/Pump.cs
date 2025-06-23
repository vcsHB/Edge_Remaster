using UnityEngine;
namespace BuildSystem.Structures
{

    public class Pump : MonoBehaviour
    {
        [SerializeField] private LayerMask _structureLayer;
        [SerializeField] private Vector2 _detectArea;


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _detectArea);
        }
    }
}