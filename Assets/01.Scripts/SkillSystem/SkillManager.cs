using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum PlayerSkill
{
    None = 0,
}

public class SkillManager : MonoSingleton<SkillManager>
{
    private Dictionary<Type, Skill> _skills;
    private List<Skill> _enabledSkillList; //��ų����Ʈ Ȱ��ȭ
    public event Action<PlayerSkill> OnSelectSkillEvent;

    protected override void Awake()
    {
        base.Awake();
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
