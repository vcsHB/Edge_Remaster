using Agents.Enemies.AI;
using UnityEngine;
namespace Agents.Enemies
{

    public class EliteEnemyAI : EnemyAI
    {
        [Header("EliteEnemy AI Setting")]
        [SerializeField] private AbilityLogicSO _abilitySO;
        public AbilityLogicSO AbilitySO => _abilitySO;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _abilitySO = _abilitySO.Clone();
            _abilitySO.InitializeOwner(_owner);
        }
    }
}