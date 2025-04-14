using UnityEngine;

namespace UI.InGame
{

    public class LevelUpPanel : UIPanel
    {
        private UpgradeContentSlot[] _slots;

        protected override void Awake()
        {
            base.Awake();
            _slots = GetComponents<UpgradeContentSlot>();

        }


    }

}