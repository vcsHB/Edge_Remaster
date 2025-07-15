using Core.DataManage;
using StageSystem;
using TMPro;
using UnityEngine;
namespace UIManage.TitleScene
{

    public class StageDetailPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _stageNameText;
        [SerializeField] private DifficultyDisplayer _difficultyDisplayer;
        [SerializeField] private WaveInfoPanel _waveInfoPanel;


        public void SetStageData(StageDataSO stageData)
        {
            _stageNameText.text = stageData.stageName;
            _difficultyDisplayer.SetDifficulty(stageData.difficulty);
            _waveInfoPanel.SetWaveInfoData(stageData.details);
            DataManager.stageDataGroup.enterStageId = stageData.id;
            DataManager.Save();
        }
    }
}