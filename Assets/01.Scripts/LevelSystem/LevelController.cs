using System;
using UnityEngine;

namespace LevelSystem
{

    public class LevelController : MonoBehaviour
    {
        public event Action<int, int> OnExpChangedEvent;
        public event Action<int> OnLevelUpEvent;
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private int _currentExp;

        private int _currentMaxExp;
        public int CurrentMaxExp => _currentExp;

        private void Awake()
        {
            RefreshMaxExp();
        }

        public void AddExp(int amount)
        {
            _currentExp += amount;
            OnExpChangedEvent?.Invoke(_currentExp, _currentMaxExp);
            CheckLevelUp();
        }


        private void CheckLevelUp()
        {
            if (_currentExp >= _currentMaxExp)
            {
                _currentExp -= _currentMaxExp;
                _currentLevel++;
                RefreshMaxExp();
            }
        }

        private void RefreshMaxExp()
        {
            _currentMaxExp = CalcMaxExp(_currentLevel);
        }

        public int CalcMaxExp(int level)
        {
            return level * 50;
        }
    }

}