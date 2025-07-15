using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BuildSystem.ResourceManage.UI
{

    public class ResourceDisplayer : MonoBehaviour
    {
        [Header("Essential Settings")]
        [SerializeField] private Image _resourceIconImage;
        [SerializeField] private TextMeshProUGUI _amountText;

        [Space(10f)]
        [Header("Additional Settings")]
        [SerializeField] private Color _enoughColor = Color.white;
        [SerializeField] private Color _shortageColor = Color.red;
        private bool _isRequireMode;
        private int _requireAmount;

        private ResourceType _currentDisplayingResourceType;
        private void Awake()
        {

        }


        public void SetResourceData(ResourceDataSO data, int amount)
        {
            _currentDisplayingResourceType = data.resourceType;
            _resourceIconImage.sprite = data.dataIconSprite;
            _isRequireMode = false;
            _amountText.color = _enoughColor;
            _amountText.text = amount.ToString();
        }

        public void SetResourceRequireData(ResourceDataSO data, int amount, int requireAmount)
        {
            _currentDisplayingResourceType = data.resourceType;
            _resourceIconImage.sprite = data.dataIconSprite;
            _isRequireMode = true;
            _requireAmount = requireAmount;
            _amountText.color = amount >= requireAmount ? _enoughColor : _shortageColor;
            _amountText.text = $"{amount}/{requireAmount}";
        }

        public void SetEnable(bool value, bool useChangeEventSubscribe = true)
        {
            if (useChangeEventSubscribe)
            {
                ResourceDataTable table = ResourceManager.Instance.GetDataTable(_currentDisplayingResourceType);
                if (value)
                    table.OnAmountChangedEvent += HandleResourceValueChanged;
                else
                    table.OnAmountChangedEvent -= HandleResourceValueChanged;

            }
            gameObject.SetActive(value);
        }

        private void HandleResourceValueChanged(int currentAmount)
        {
            if (_isRequireMode)
            {
                _amountText.color = currentAmount >= _requireAmount ? _enoughColor : _shortageColor;
                _amountText.text = $"{currentAmount}/{_requireAmount}";
            }
            else
            {
                _amountText.text = currentAmount.ToString();
            }
        }



    }
}