using System;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerSkill
{
    None = 0,
    EnergyBall = 1,
    EnergyBim = 2,
    SordBoomerang = 3,
    DataBarier = 4,
    KnifeSharpening = 5,
    HighSpeedAttack = 6

}

public class SkillManager : MonoSingleton<SkillManager>
{
    private Dictionary<Type, Skill> _skills;
    private List<Skill> _enabledSkillList; //��ų����Ʈ Ȱ��ȭ
    public event Action<PlayerSkill> OnSelectSkillEvent;

    private void Awake()
    {
        _skills = new Dictionary<Type, Skill>();
        _enabledSkillList = new List<Skill>();

        foreach (PlayerSkill skillEnum in Enum.GetValues(typeof(PlayerSkill)))
        {
            if (skillEnum == PlayerSkill.None) continue; //�Ѿ��

            Skill skillCompo = GetComponent($"{skillEnum.ToString()}Skill") as Skill; 
            Type type = skillCompo.GetType(); //�ش� �ν��Ͻ��� Ŭ������ Ÿ���� �����´�.
            _skills.Add(type, skillCompo);
        }
    }

    public void AddEnableSkill(Skill skill)
    {
        _enabledSkillList.Add(skill);
    }

    private void Update()
    {
        foreach (Skill skill in _enabledSkillList)
        {
            skill.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GetSkill(PlayerSkill.EnergyBall).UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GetSkill(PlayerSkill.EnergyBim).UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GetSkill(PlayerSkill.SordBoomerang).UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GetSkill(PlayerSkill.DataBarier).UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GetSkill(PlayerSkill.KnifeSharpening).UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GetSkill(PlayerSkill.HighSpeedAttack).UseSkill();
        }
    }

    public T GetSkill<T>() where T : Skill
    {
        Type t = typeof(T);
        if (_skills.TryGetValue(t, out Skill target))
        {
            return target as T;
        }
        return null;
    }

    public Skill GetSkill(PlayerSkill skillEnum)
    {
        Type type = Type.GetType($"{skillEnum.ToString()}Skill");
        if (type == null) return null;

        if (_skills.TryGetValue(type, out Skill target))
        {
            return target;
        }
        return null;
    }

    public void SelectSkill(PlayerSkill skillType)
    {
        OnSelectSkillEvent?.Invoke(skillType);
    }
}
