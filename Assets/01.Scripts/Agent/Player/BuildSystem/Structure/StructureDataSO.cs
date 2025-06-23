using BuildSystem.ResourceManage;
using UnityEngine;
namespace BuildSystem.Structures
{
    [CreateAssetMenu(menuName = "SO/StructureData")]
    public class StructureDataSO : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        public Structure structurePrefab;

        public string structureName;
        [TextArea] public string description;
        public Sprite previewImage;
        public float maxHealth = 100;
        
        [Space(10f)]
        [Header("Build Settings")]
        public ResourceData[] requireResources;


    }
}