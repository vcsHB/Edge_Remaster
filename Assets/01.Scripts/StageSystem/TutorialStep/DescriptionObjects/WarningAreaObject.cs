using DG.Tweening;
using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class WarningAreaObject : DescriptionObject
    {
        [SerializeField] private float _enableDuration = 0.5f;
        [SerializeField] private float _enableScale = 9f;
        
        public override void Close()
        {
            transform.DOScale(0f, _enableDuration);
        }

        public override void Open()
        {
            transform.DOScale(_enableScale, _enableDuration);
        }
    }
}