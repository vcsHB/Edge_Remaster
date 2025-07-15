using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
namespace UI.TitleScene
{

    public class UpgradeRequireCostSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _costAmountText;
        [SerializeField] private Color _enoughColor = Color.white;
        [SerializeField] private Color _shortageColor = Color.red;

        public void SetAmount(int amount, int require)
        {
            bool enable = require > 0;
            gameObject.SetActive(enable);

            if (enable)
            {
                bool isEnough = amount >= require;
                _costAmountText.color = isEnough ? _enoughColor : _shortageColor;
                _costAmountText.text = $"{amount.ToString()}/{require}";
            }

        }
    }
}