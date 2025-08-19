using SoundManage;
using UnityEngine;

namespace FeedbackSystem
{

    public class SoundFeedback : Feedback
    {

        [SerializeField] private SoundSO _soundSO;

        public override void CreateFeedback()
        {
            SoundPlayer soundPlayer = SoundController.Instance.PlaySound(_soundSO, transform.position);
            if (soundPlayer == null)
            {
                Debug.Log("???");
                return;
            }
        }

        public override void FinishFeedback()
        {
        }
    }
}