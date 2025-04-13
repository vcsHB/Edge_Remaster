using Agents.Players;
using UnityEngine;


namespace SkillSystem
{

    public delegate void CooldownInfoEvent(float current, float total);

    public abstract class Skill : MonoBehaviour
    {

        public bool skillEnabled = false; //�� ��ų�� Ȱ��ȭ�Ǿ��°�?
        [SerializeField] protected float _cooldown; //��ų�� ��Ÿ��
        [SerializeField] protected bool _isAutoSkill; //�ڵ��ߵ� ��ų�̳�?

        [HideInInspector] public Player player;
        protected float _cooldownTimer;
        public event CooldownInfoEvent OnCooldownEvent;

        public LayerMask whatIsEnemy;

        public void UnlockSkill()
        {
            if (skillEnabled) return;

            skillEnabled = true;
            if (_isAutoSkill)
            {
                SkillManager.Instance.AddEnableSkill(this);
            }
        }

        protected virtual void Start()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        protected virtual void Update()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer <= 0) // 0���� �۴ٸ�
                {
                    _cooldownTimer = 0;
                }
                OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
            }
        }

        public virtual bool UseSkill()
        {
            if (_cooldownTimer > 0 || skillEnabled == false) return false;

            _cooldownTimer = _cooldown;
            return true;
        }
    }

}
