using UnityEngine;
namespace UIManage.InGame
{

    public class SelectionPanel : UIPanel
    {
        
        public void SetPanelState(bool isActive)
        {
            if (isActive)
                Open();
            else
                Close();
        }
        
    }
}