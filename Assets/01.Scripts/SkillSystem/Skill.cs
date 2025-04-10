using Agents.Players;
using UnityEngine;

//쿨타임이 몇초남았는지를 알려주는 델리게이트를 만들었다.
public delegate void CooldownInfoEvent(float current, float total);

public abstract class Skill : MonoBehaviour
{

    public bool skillEnabled = false; //이 스킬이 활성화되었는가?
    [SerializeField] protected float _cooldown; //스킬의 쿨타임
    [SerializeField] protected bool _isAutoSkill; //자동발동 스킬이냐?

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
            if (_cooldownTimer <= 0) // 0보다 작다면
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
