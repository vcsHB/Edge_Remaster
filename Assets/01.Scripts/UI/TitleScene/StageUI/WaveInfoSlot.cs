using StageSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage.TitleScene
{

    public class WaveInfoSlot : MonoBehaviour
    {
        [SerializeField] private Image _waveInfoIconImage;
        [SerializeField] private TextMeshProUGUI _waveInfoTitleText;
        [SerializeField] private TextMeshProUGUI _waveInfoContentText;


        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }
        public void SetData(StageDetailOption data)
        {
            _waveInfoContentText.text = data.detailContent;
            _waveInfoTitleText.text = data.detailName;
            _waveInfoTitleText.color = data.color;
            _waveInfoIconImage.color = data.color;
            _waveInfoIconImage.sprite = data.detailIcon;

        }
    }
}