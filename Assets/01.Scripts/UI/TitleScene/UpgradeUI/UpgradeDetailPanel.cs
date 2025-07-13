using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpgradeSystem;
namespace UIManage.TitleScene
{

    public class UpgradeDetailPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeRequireCostPanel _costDisplayer;
        [SerializeField] private Button _installButton;
        private Image _installButtonImage;
        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;
        private UpgradeDataSO _upgradeData;
        private bool _isEnough;

        public event Action<UpgradeDataSO> OnUpgradeInstallEvent;


        private void Awake()
        {
            _installButtonImage = _installButton.GetComponent<Image>();
            _installButton.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            if (_isEnough)
                OnUpgradeInstallEvent?.Invoke(_upgradeData);
        }

        public void SetData(UpgradeDataSO upgradeData, int vertexCrystal, int metaCrystal)
        {
            _upgradeData = upgradeData;
            _costDisplayer.SetAmountData(
                new int[2]{
                    vertexCrystal,
                    metaCrystal

                },
                new int[2] {
                    upgradeData.requireVertexCost,
                    upgradeData.requireMetaCost
                }
            );
            _isEnough =
                vertexCrystal >= upgradeData.requireVertexCost &&
                metaCrystal >= upgradeData.requireMetaCost;
            _installButtonImage.color = _isEnough ? _enableColor : _disableColor;

        }


    }
}