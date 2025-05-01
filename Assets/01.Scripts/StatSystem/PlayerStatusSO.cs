using UnityEngine;
namespace StatSystem
{
   


    [CreateAssetMenu(menuName = "SO/Status/PlayerStatus")]
    public class PlayerStatusSO : StatusSO
    {


        public Stat edgeSlideSpeed;
        public Stat edgeMoveCooltime;
        public Stat objectDamage;
        public Stat objectAttackSpeed;
        public Stat objectHealth;
        public Stat bonusCrystal;
        public Stat bonusPolygon;

        protected override void OnEnable()
        {
            base.OnEnable();
            statDictionary.Add(StatType.EdgeSlideSpeed, edgeSlideSpeed);
            statDictionary.Add(StatType.EdgeMoveCooltime, edgeMoveCooltime);
            statDictionary.Add(StatType.ObjectAttackDamage, objectDamage);
            statDictionary.Add(StatType.ObjectAttackSpeed, objectAttackSpeed);
            statDictionary.Add(StatType.ObjectHealth, objectHealth);
            statDictionary.Add(StatType.BonusCrystal, bonusCrystal);
            statDictionary.Add(StatType.BonusPolygon, bonusPolygon);
        }
    }
}