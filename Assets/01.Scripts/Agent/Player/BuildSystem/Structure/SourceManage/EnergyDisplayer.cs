using TMPro;
using UnityEngine;
namespace BuildSystem.ResourceManage
{

    public class EnergyDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _amountText;
        private SpriteRenderer _visualRenderer;


        private void Awake()
        {
            _visualRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }
        public void SetEnable(bool value)
        {
            _visualRenderer.gameObject.SetActive(value);
            _amountText.enabled = value;
        }

        public void SetEnergyAmount(float amount, float max)
        {
            _amountText.text = $"{(Mathf.Clamp01(amount / max) * 100f).ToString("0.0")}%";
        }
    }
}