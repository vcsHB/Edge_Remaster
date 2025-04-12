using System;
using LevelSystem;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{

    public class ExpGauge : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private LevelController _ownerController;
        private void Awake() {
            _ownerController.OnExpChangedEvent += HandleExpChange;
        }

        private void HandleExpChange(int current, int max)
        {
            _fillImage.fillAmount = (float)current / max;
        }
    }
}