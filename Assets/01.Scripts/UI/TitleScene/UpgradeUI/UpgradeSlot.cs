using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpgradeSystem;
namespace UIManage.TitleScene
{

    public class UpgradeSlot : MonoBehaviour
    {
        public event Action<UpgradeDataSO> OnUpgradeSelectEvent;

        [SerializeField] private UIPanel _installedPanel;
        [SerializeField] private TextMeshProUGUI _upgradeNameText;
        [SerializeField] private TextMeshProUGUI _upgradeDescriptionText;
        [SerializeField] private Image _iconImage;

        private Button _button;
        [SerializeField] private UpgradeDataSO _upgradeData;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleClickEvent);
        }

        private void HandleClickEvent()
        {
            OnUpgradeSelectEvent?.Invoke(_upgradeData);
        }

        public void SetData(UpgradeDataSO upgradeData, bool isInstalled)
        {
            _upgradeData = upgradeData;
            _upgradeNameText.text = _upgradeData.upgradeName;
            _upgradeDescriptionText.text = _upgradeData.upgradeDescription;
            _iconImage.sprite = _upgradeData.upgradeIcon;

            if (isInstalled)
                _installedPanel.Open();
            else
                _installedPanel.Close();
        }

        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}