using System;
using Combat;
using SkillSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{
    public class UpgradeContentSlot : MonoBehaviour
    {
        public event Action<int> OnSelectedEvent;
        public int Id => _indexId;
        private int _indexId;
        [field: SerializeField] public PowerUpSO PowerUp { get; private set; }
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _upgradeNameText;
        [SerializeField] private SelectionPanel _selectViewPanel;
        [SerializeField] private SelectionPanel _unselectViewPanel;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleSelectPowerUp);
        }

        public void Initialize(int id)
        {
            _indexId = id;
        }


        public void SetUpgradeInfo(PowerUpSO powerUp)
        {
            PowerUp = powerUp;
            _unselectViewPanel.SetPanelState(false);
            _selectViewPanel.SetPanelState(false);
            UpdateUI();

        }
        public void SetSelect(bool isSelected)
        {
            _selectViewPanel.SetPanelState(isSelected);
            _unselectViewPanel.SetPanelState(!isSelected);
        }
        private void HandleSelectPowerUp()
        {
            PowerUp.effectList.ForEach(effect => effect.UseEffect());
            OnSelectedEvent?.Invoke(_indexId);
        }

        private void UpdateUI()
        {
            if (_upgradeNameText != null)
                _upgradeNameText.text = PowerUp.title;

            if (_descriptionText != null)
                _descriptionText.text = PowerUp.description;

            if (_iconImage != null)
                _iconImage.sprite = PowerUp.icon;
        }

        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (PowerUp != null)
            {
                UpdateUI();
            }
        }
#endif
    }
}