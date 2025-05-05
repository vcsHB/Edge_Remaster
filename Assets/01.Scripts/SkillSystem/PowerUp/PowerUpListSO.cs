using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{

    [CreateAssetMenu(menuName = "SO/Item/PowerUp/List")]
    public class PowerUpListSO : ScriptableObject
    {
        public List<PowerUpSO> list = new List<PowerUpSO>();

    }
}
