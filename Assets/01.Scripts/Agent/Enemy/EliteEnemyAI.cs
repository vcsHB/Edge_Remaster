using System;
using Agents.Enemies.AI;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{
    [System.Serializable]
    public struct AbilitySet
    {
        public AbilityLogicSO abilitySO;
        public UnityEvent OnAbilityStartEvent;

    }
    public class EliteEnemyAI : EnemyAI
    {

        public event Action<float, float> OnCooltimeUpdateEvent;// <current, max>
        public event Action OnAbilityCompleteEvent;
        [Header("EliteEnemy AI Setting")]
        [SerializeField] private AbilitySet[] _abilitys;
        [SerializeField] private bool _canDuplication; // condition of : Use A Skill -> Use A Skill
        private int _currentAbilityIndex;
        private float _currentCooltime;
        private float _coolingSpeed = 1f;
        public AbilityLogicSO CurrentAbility => _abilitys[_currentAbilityIndex].abilitySO;
        private bool _isAbilityComplete = true;
        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            for (int i = 0; i < _abilitys.Length; i++)
            {
                _abilitys[i].abilitySO = _abilitys[i].abilitySO.Clone();
                AbilityLogicSO ability = _abilitys[i].abilitySO;
                ability.InitializeOwner(_owner);
                ability.OnAbilityCompleteEvent += HandleAbilityCompelete;
                _detectLogic.OnDetectEvent += ability.HandleDetect;
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

        public bool UpdateAbility()
        {
            if (!_isAbilityComplete) return false;

            _currentCooltime += Time.deltaTime * _coolingSpeed;
            float cooltime = CurrentAbility.useCooltime;
            OnCooltimeUpdateEvent?.Invoke(_currentCooltime, cooltime);
            if (cooltime > _currentCooltime) return false;
            if (!CurrentAbility.HandleUseAbility()) return false;

            _abilitys[_currentAbilityIndex].OnAbilityStartEvent?.Invoke();
            _isAbilityComplete = false;
            _currentCooltime = 0f;
            return true;
        }

        private void SelectNextAbility()
        {
            _currentAbilityIndex = UnityEngine.Random.Range(0, _abilitys.Length);
        }
    }
}