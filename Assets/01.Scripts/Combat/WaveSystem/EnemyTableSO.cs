using System.Collections.Generic;
using UnityEngine;
namespace Combat.WaveSystem
{

    [CreateAssetMenu(menuName = "SO/WaveSystem/EnemySO")]
    public class EnemyTableSO : ScriptableObject
    {
        public List<EnemySO> datas;
    }
}