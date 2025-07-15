using UnityEngine;
namespace StageSystem
{
    [CreateAssetMenu(menuName = "SO/Stage/StageDetail")]
    public class StageDetailOption : ScriptableObject
    {
        public string detailName;
        public Sprite detailIcon;
        public Color color;

        public string detailContent;
        
    }
}