using UnityEngine;
namespace BuildSystem.ResourceManage
{
    [CreateAssetMenu(menuName = "SO/Resource")]
    public class Resource : ScriptableObject
    {
        public ResourceType resourceType;
        public Sprite resourceIcon;
        public string resourceName;
        public string resourceDescription;
        
    }
}