using System.Collections.Generic;
using UnityEngine;
namespace Agents.Enemies.UtilItem
{
    [System.Serializable]
    public struct ItemTable
    {
        public EnemyItemSO item;
        [Range(0, 100f)] public float useRate;
    }

    public class EnemyItemController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private ItemTable[] _items;
        [SerializeField] private int _useAmount = 1;


        public void Initialize(Agent agent)
        {
        }
        public void AfterInit()
        {
        }

        
        public void Dispose()
        {
        }


        public void ApplyItem()
        {
            List<ItemTable> shuffled = new List<ItemTable>(_items);
            Shuffle(shuffled);

            int usedCount = 0;
            foreach (var item in shuffled)
            {
                if (usedCount >= _useAmount) break;

                float rand = Random.Range(0f, 100f);
                if (rand <= item.useRate)
                {
                    if (item.item != null && item.item.itemPrefab != null)
                    {
                        Instantiate(item.item.itemPrefab, transform);
                        usedCount++;
                    }
                }
            }
        }



        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }


    }
}
