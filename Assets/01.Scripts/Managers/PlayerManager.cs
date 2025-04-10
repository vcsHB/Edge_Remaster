using Agents.Players;
using UnityEngine;
namespace Core
{

    public class PlayerManager : MonoSingleton<PlayerManager>
    {

        [field:SerializeField] public Player Player {get; private set;}

        

    }
}