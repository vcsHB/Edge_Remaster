using SkillSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{
    public class UpgradeContentSlot : MonoBehaviour
    {
        [SerializeField] private Image _contentImage;
        [SerializeField] private TextMeshProUGUI _upgradeNameText;
        private PowerUpSO _powerUpSO;



        public void SetUpgradeInfo(PowerUpSO powerUp)
        {
            _powerUpSO = powerUp;
            _contentImage.sprite = _powerUpSO.icon;
            _upgradeNameText.text = powerUp.title;
            
        }

    }
}