using System;
using UI.InGame;
using UnityEngine;

namespace LevelSystem
{

    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] private LevelUpPanel _levelUpPanel;
        public event Action<int, int> OnCrystalChangedEvent;
        public event Action<int> OnLevelUpEvent;
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private int _currentCrystal;

        private int _currentMaxCrystal;
        public int CurrentMaxCrystal => _currentCrystal;

        protected override void Awake()
        {
            base.Awake();
            RefreshMaxCrystal();
        }

        public void AddCrystal(int amount)
        {
            _currentCrystal += amount;
            OnCrystalChangedEvent?.Invoke(_currentCrystal, _currentMaxCrystal);
            CheckLevelUp();
        }


        private void CheckLevelUp()
        {
            if (_currentCrystal >= _currentMaxCrystal)
            {
                _currentCrystal -= _currentMaxCrystal;
                _currentLevel++;
                _levelUpPanel.Open();
                RefreshMaxCrystal();
            }
        }

        private void RefreshMaxCrystal()
        {
            _currentMaxCrystal = CalcMaxCrystal(_currentLevel);
        }

        public int CalcMaxCrystal(int level)
        {
            return level * 10;
        }
    }

}