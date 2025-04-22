using UnityEngine;
namespace Agents.Enemies
{

    public class EnemyRenderer : MonoBehaviour, IAgentComponent
    {
        protected Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Initialize(Agent agent)
        {

        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void SetParam(int hash, bool value)
        {
            _animator.SetBool(hash, value);
        }

    }
}