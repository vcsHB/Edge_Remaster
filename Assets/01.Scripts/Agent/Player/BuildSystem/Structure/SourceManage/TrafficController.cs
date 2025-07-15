using UnityEngine;
namespace BuildSystem.ResourceManage
{

    public class TrafficController : MonoBehaviour, ITrafficGainable
    {
        [SerializeField] private float _currentTraffic;
        
        public void ApplyTraffic(float amount)
        {

        }
    }
}