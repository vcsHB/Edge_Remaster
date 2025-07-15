using UnityEngine;
namespace Agents.Enemies.UtilItem
{


    public abstract class EnemyItem : MonoBehaviour
    {
        private int _ownerLevel;

        public virtual void SetLevel(int level)
        {
            _ownerLevel = level;
        }
        public abstract void ResetItem();
        
    }
}