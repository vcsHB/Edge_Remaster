using Combat;
using UnityEngine;
namespace RewardSystem
{

    public class RewardDropper : MonoBehaviour
    {
        [SerializeField] private RewardSO _rewardData;
        private Health _healthCompo;

        private void Awake()
        {
            _healthCompo = GetComponent<Health>();
            _healthCompo.OnDieEvent.AddListener(DropReward);
        }
        public void DropReward()
        {
            PoolManager.Instance.Pop(ObjectPooling.PoolingType.SoundPlayer);

        }
    }
}