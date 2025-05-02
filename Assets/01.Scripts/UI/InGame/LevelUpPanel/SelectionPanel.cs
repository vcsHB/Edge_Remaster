using UnityEngine;
namespace UI.InGame
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