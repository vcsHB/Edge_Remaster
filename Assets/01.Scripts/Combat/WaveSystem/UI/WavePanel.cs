using System.Runtime.InteropServices;
using Combat.WaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage.InGame
{

    public class WavePanel : UIPanel
    {
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private Image _timeGaugeImage;
        [SerializeField] private TextMeshProUGUI _leftTimeText;

        protected override void Awake()
        {
            base.Awake();
            _waveManager.OnWaveCycleInitEvent += Open;
            _waveManager.OnWaveCompleteEvent.AddListener(Open);
            _waveManager.OnWaveStartEvent.AddListener(Close);

            _waveManager.OnWaveLeftTimeEvent += SetLeftTime;
        }

        public void SetLeftTime(int second, float ratio)
        {
            _leftTimeText.text = $"{second.ToString()}s";
            _timeGaugeImage.fillAmount = ratio;

        }
    }
}