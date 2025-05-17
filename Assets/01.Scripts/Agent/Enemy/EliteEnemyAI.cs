using System;
using Agents.Enemies.AI;
using UnityEngine;
namespace Agents.Enemies
{

    public class EliteEnemyAI : EnemyAI
    {
        public event Action<float, float> OnCooltimeUpdateEvent;// <current, max>
        public event Action OnAbilityCompleteEvent;
        [Header("EliteEnemy AI Setting")]
        [SerializeField] private AbilityLogicSO[] _abilitys;
        [SerializeField] private bool _canDuplication; // condition of : Use A Skill -> Use A Skill
        private int _currentAbilityIndex;
        private float _currentCooltime;
        private float _coolingSpeed = 1f;
        public AbilityLogicSO CurrentAbility => _abilitys[_currentAbilityIndex];
        private bool _isAbilityComplete = true;
        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            for (int i = 0; i < _abilitys.Length; i++)
            {
                _abilitys[i] = _abilitys[i].Clone();
                _abilitys[i].InitializeOwner(_owner);
                _abilitys[i].OnAbilityCompleteEvent += HandleAbilityCompelete;
            }
        }

        private void HandleAbilityCompelete()
        {
            SelectNextAbility();
            _isAbilityComplete = true;
            OnAbilityCompleteEvent?.Invoke();

        }

        public void SetCoolingSpeed(float newSpeed = 1f)
        {
            _coolingSpeed = newSpeed;
        }

        public void UpdateAbility()
        {
            if (!_isAbilityComplete) return;

            _currentCooltime += Time.deltaTime * _coolingSpeed;
            float cooltime = CurrentAbility.useCooltime;
            OnCooltimeUpdateEvent?.Invoke(_currentCooltime, cooltime);
            if (cooltime > _currentCooltime) return;

            if (CurrentAbility.HandleUseAbility())
            {
                _isAbilityComplete = false;
                _currentCooltime = 0f;
            }
        }

        private void SelectNextAbility()
        {
            _currentAbilityIndex = UnityEngine.Random.Range(0, _abilitys.Length);
        }
    }
}