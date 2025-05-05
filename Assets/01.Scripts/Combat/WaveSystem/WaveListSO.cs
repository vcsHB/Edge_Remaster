using UnityEngine;
namespace Combat.WaveSystem
{
    [CreateAssetMenu(menuName ="SO/WaveSystem/WaveListSO")]
    public class WaveListSO : ScriptableObject
    {
        public WaveSO[] waves;

        
    }
}