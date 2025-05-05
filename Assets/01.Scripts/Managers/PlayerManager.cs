using Agents.Players;
using ObjectManage;
using UnityEngine;
namespace Core
{

    public class PlayerManager : MonoSingleton<PlayerManager>
    {

        [field:SerializeField] public Player Player {get; private set;}

        
        public void ForceMovePlayer(MovePoint movePoint, float duration)
        {
            Player.ForceMoveToPoint(movePoint, duration);
        }
    }
}