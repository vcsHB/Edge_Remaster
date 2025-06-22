using BuildSystem.ResourceManage;
using UnityEngine;
namespace BuildSystem.Structures
{
    [CreateAssetMenu(menuName ="SO/StructureData")]
    public class StructureDataSO : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        public Structure structurePrefab;

        public string structureName;
        public Sprite previewImage;
        public ResourceData[] requireResources;
        [TextArea] public string description;
        

    }
}