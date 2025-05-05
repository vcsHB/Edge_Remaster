using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{

    [CreateAssetMenu(menuName = "SO/Item/PowerUp/EffectList")]
    public class PowerUpEffectListSO : ScriptableObject
    {
        public List<PowerUpEffectSO> list = new List<PowerUpEffectSO>();

    }

}