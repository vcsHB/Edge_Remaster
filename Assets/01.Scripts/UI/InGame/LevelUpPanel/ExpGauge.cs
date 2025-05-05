using System;
using LevelSystem;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{

    public class ExpGauge : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private LevelManager _ownerController;
        private void Awake() {
            _ownerController.OnCrystalChangedEvent += HandleExpChange;
        }

        private void HandleExpChange(int current, int max)
        {
            _fillImage.fillAmount = (float)current / max;
        }
    }
}