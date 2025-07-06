using UnityEngine;
namespace BuildSystem.DataManage
{
    [CreateAssetMenu(menuName ="SO/Datas/DataCategory")]
    public class DataCategory : ScriptableObject
    {
        public string categoryName;
        [TextArea] public string categoryDescription;

        public Sprite categoryIcon;

        
    }
}