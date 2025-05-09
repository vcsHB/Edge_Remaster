using Agnets.Enemies;
using UnityEngine;
namespace Agents.Enemies.AI
{
    
    public abstract class ComabtLogicSO : ScriptableObject
    {
        protected Enemy _owner;
        protected EnemyAttackController _attackController;
        protected DetectData _targetData;


        public void HandleDetect(DetectData detectData)
        {
            _targetData = detectData;
        }
        public void SetOwner(Enemy owner)
        {
            _owner = owner;
            _attackController = _owner.GetCompo<EnemyAttackController>();
        }

        public ComabtLogicSO Clone() => Instantiate(this);
    }
}