using System.Collections.Generic;
using UnityEngine;

namespace Agents.Players.Combat
{

    public class PlayerSkillController : MonoBehaviour, IAgentComponent
    {
        private Player _player;

        private Queue<PlayerSkill> _skillQueue = new();
        private Dictionary<PlayerSkill, Skill> _skillDictionary = new Dictionary<PlayerSkill, Skill>();
        private PlayerSkill[] _indexer = null;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            SkillManager.Instance.OnSelectSkillEvent += HandleGetSkill;

            _player.PlayerInput.OnUseSkill1Event += HandleUseSkill1;
            _player.PlayerInput.OnUseSkill2Event += HandleUseSkill2;
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
            SkillManager.Instance.OnSelectSkillEvent -= HandleGetSkill;

            _player.PlayerInput.OnUseSkill1Event -= HandleUseSkill1;
            _player.PlayerInput.OnUseSkill2Event -= HandleUseSkill2;
        }

        public void HandleGetSkill(PlayerSkill skillType)
        {
            if (_skillDictionary.ContainsKey(skillType))
                return;
            _skillQueue.Enqueue(skillType);
            _skillDictionary.Add(skillType, SkillManager.Instance.GetSkill(skillType));
            if (_skillQueue.Count > 2)
            {
                PlayerSkill deleteType = _skillQueue.Dequeue();
                _skillDictionary.Remove(deleteType);
            }
            _indexer = _skillQueue.ToArray();

        }


        private void HandleUseSkill1()
        {
            if(_indexer == null) return;
            if (_indexer.Length < 1) return;
            _skillDictionary[_indexer[0]].UseSkill();
        }

        private void HandleUseSkill2()
        {
            if(_indexer == null) return;
            if (_indexer.Length < 2) return;
            _skillDictionary[_indexer[1]].UseSkill();

        }



    }

}