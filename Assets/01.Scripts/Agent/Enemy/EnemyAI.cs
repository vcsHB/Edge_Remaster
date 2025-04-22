using System;
using Agents.Enemies.AI;
using UnityEngine;

namespace Agents.Enemies
{

    public class EnemyAI : MonoBehaviour, IAgentComponent
    {
        [SerializeField] protected EnemyAILogicSO _logic;
        public EnemyAILogicSO Logic => _logic;

        // Properties
        public bool IsTargeted => _detectData.isTargeted;
        public Vector2 TargetDirection => _detectData.targetDirection;
        public float DistanceToTarget => _detectData.distanceToTarget;
        public Vector2 TargetPos => _detectData.targetPos;
        protected DetectData _detectData;


        protected Enemy _owner;
        public void Initialize(Agent agent)
        {
            _owner = agent as Enemy;
            _logic = _logic.Clone();
            _logic.InitializeOwner(_owner.transform);
            //_enemyMovement = agent.GetCompo<EnemyMovement>();
        }

        private void Update()
        {
            _detectData = _logic.DetectTarget();
        }

        private void OnDestroy()
        {
            Destroy(_logic);

        }
        public void AfterInit() { }
        public void Dispose() { }








    }

}