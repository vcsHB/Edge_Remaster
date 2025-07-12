using DG.Tweening;
using StageSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage.TitleScene
{

    public class DifficultyDisplayer : MonoBehaviour
    {
        [SerializeField] private Image _mainGauge;
        [SerializeField] private TextMeshProUGUI _difficultyText;
        [SerializeField] private Image[] _subGauges;
        [SerializeField] private float _mainGaugeFillDuration;
        [SerializeField] private float _subGaugeGaugesFillDuration;

        public void SetDifficulty(StageDifficultyDataSO difficulty)
        {
            _mainGauge.color = difficulty.difficultyColor;
            _mainGauge.fillAmount = 0f;
            _difficultyText.text = difficulty.difficultyName;
            _mainGauge.DOFillAmount(difficulty.difficultyLevel, _mainGaugeFillDuration);
            for (int i = 0; i < _subGauges.Length; i++)
            {
                Image gauge = _subGauges[i];
                gauge.color = difficulty.difficultyColor;
                gauge.fillAmount = 0f;
                gauge.DOFillAmount(difficulty.difficultyLevel, _subGaugeGaugesFillDuration);
            }
        }
    }
}