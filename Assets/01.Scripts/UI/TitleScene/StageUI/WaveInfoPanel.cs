using StageSystem;
using UnityEngine;
namespace UIManage.TitleScene
{

    public class WaveInfoPanel : MonoBehaviour
    {
        [SerializeField] private WaveInfoSlot[] _slots;

        public void SetWaveInfoData(StageDetailOption[] options)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].SetData(options[i]);
            }
        }
    }
}