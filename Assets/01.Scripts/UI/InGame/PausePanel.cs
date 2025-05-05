using UnityEngine;

namespace UI.InGame
{

    public class PausePanel : UIPanel, IWindowTogglable
    {
        public void Toggle()
        {
            if (_isActive)
                Close();
            else
                Open();
        }

        public override void Open()
        {
            base.Open();
        
        }

        public override void Close()
        {
            base.Close();

        }
    }

}