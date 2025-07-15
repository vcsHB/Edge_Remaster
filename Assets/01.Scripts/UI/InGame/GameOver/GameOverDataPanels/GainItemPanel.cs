using BuildSystem.ResourceManage;
using TMPro;
using UnityEngine;
namespace UIManage.InGame
{

    public class GainItemPanel : MonoBehaviour
    {
        [SerializeField] private ResourceManager _resourceManager;
        [SerializeField] private TextMeshProUGUI _vertexAmountText;
        [SerializeField] private TextMeshProUGUI _mataAmountText;
        [SerializeField] private TextMeshProUGUI _ketherAmountText;

        public void UpdateGains()
        {
            _vertexAmountText.text = _resourceManager.GetAmount(ResourceType.VertexData).ToString();
        }
    }
}