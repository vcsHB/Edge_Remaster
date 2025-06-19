using TMPro;
using UnityEngine;

namespace UIManage
{

    public class DepthText : MonoBehaviour
    {
        [SerializeField] private Transform _ownerTrm;
        [SerializeField] private TextMeshProUGUI _depthText;
        private bool _isEnabled;
        private void Awake()
        {

        }

        public void SetEnabled(bool value)
        {
            _isEnabled = value;
            
        }

        private void Update()
        {

        }

        private void SetDepthText(float depth)
        {
            _depthText.text = $"DEPTH :{(int)(depth * 10)}";
        }
    }
}
