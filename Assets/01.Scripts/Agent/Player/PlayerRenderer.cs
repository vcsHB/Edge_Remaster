using UnityEngine;

namespace Agents.Players.FSM
{

    public class PlayerRenderer : MonoBehaviour, IAgentComponent
    {
        private Player _player;
        [SerializeField] private GameObject _moveLight;
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void SetDisableMoveLight()
        {
            _moveLight.SetActive(false);
        }

        public void SetEnableMoveLight()
        {
            _moveLight.SetActive(true);
        }

    }

}