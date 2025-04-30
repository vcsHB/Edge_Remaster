using Agents;
using Agents.Enemies;
using Combat;
using UnityEngine;
namespace RewardSystem
{

    public class RewardDropper : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private RewardSO _rewardData;
        [SerializeField, Range(2, 10)] private int _dropSplitAmountRandomize;
        private Health _healthCompo;
        private Enemy _enemy;

        private void Awake()
        {
            _healthCompo = GetComponent<Health>();
            _healthCompo.OnDieEvent.AddListener(DropReward);
        }
        public void DropReward()
        {
            int totalCrystal = _enemy.EnemyLevel * _rewardData.GetCrystalAmount();
            int splitAmount = Random.Range(2, _dropSplitAmountRandomize);
            int splitedCrystal = totalCrystal / splitAmount;
            for (int i = 0; i < splitAmount; i++)
            {
                Crystal crystal = PoolManager.Instance.Pop(ObjectPooling.PoolingType.CrystalObject) as Crystal;
                crystal.Drop(transform.position, splitedCrystal, Random.insideUnitCircle);
            }
        }

        public void Initialize(Agent agent)
        {
            _enemy = agent as Enemy;

        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}