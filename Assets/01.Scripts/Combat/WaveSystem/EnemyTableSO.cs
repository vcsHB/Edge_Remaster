using System.Collections.Generic;
using UnityEngine;
namespace Combat.WaveSystem
{

    [CreateAssetMenu(menuName = "SO/WaveSystem/EnemyTableSO")]
    public class EnemyTableSO : ScriptableObject
    {
        public List<EnemySO> datas;

    }
}