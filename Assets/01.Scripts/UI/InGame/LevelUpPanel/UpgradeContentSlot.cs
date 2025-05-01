using System;
using SkillSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{
    public class UpgradeContentSlot : MonoBehaviour
    {
        public event Action OnSelectedEvent;
        [field: SerializeField] public PowerUpSO PowerUp { get; private set; }
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _upgradeNameText;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleSelectPowerUp);
        }


        public void SetUpgradeInfo(PowerUpSO powerUp)
        {
            PowerUp = powerUp;
            UpdateUI();

        }
        private void HandleSelectPowerUp()
        {
            PowerUp.effectList.ForEach(effect => effect.UseEffect());
            OnSelectedEvent?.Invoke();
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