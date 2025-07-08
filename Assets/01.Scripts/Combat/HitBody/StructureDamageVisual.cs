using System;
using UnityEngine;
namespace Combat
{

    public class StructureDamageVisual : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _durabilityAlphaRenderer;
        [SerializeField] private GameObject _linePanelObject;
        [SerializeField, Range(0f, 1f)] private float _maxAlpha = 0.6f;
        private Health _owner;
        private Transform _visualTrm;

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _owner = GetComponentInParent<Health>();
            _owner.OnHealthChangedValueEvent += HandleHealthValueChanged;
        }


        private void HandleHealthValueChanged(float current, float max)
        {
            float ratio = current / max;
            if (ratio > 0.7f)
            {
                _visualTrm.gameObject.SetActive(false);
                
                return;
            }
            _visualTrm.gameObject.SetActive(true);
            
            _linePanelObject.SetActive(ratio < 0.9f);

            Color color = _durabilityAlphaRenderer.color;
            color.a = ratio * _maxAlpha;
            _durabilityAlphaRenderer.color = color;


        }
    }
}