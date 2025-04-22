using Agents.Enemies.AI;
using UnityEngine;

namespace Agents.Enemies
{

    public class EnemyAI : MonoBehaviour, IAgentComponent
    {
        [SerializeField] protected EnemyAILogicSO _logic;
        public EnemyAILogicSO Logic => _logic;

        public void Initialize(Agent agent)
        {
            _logic = _logic.Clone();
            //_enemyMovement = agent.GetCompo<EnemyMovement>();
        }
        public void AfterInit() { }
        public void Dispose() { }






    }

}