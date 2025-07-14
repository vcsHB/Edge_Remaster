using System;
using UnityEngine;
namespace StageSystem
{

    public class StageLevel : MonoBehaviour
    {
        public event Action OnMapInitOverEvent;

        protected void InvokeMapInitOver()
        {
            OnMapInitOverEvent?.Invoke();
        }
    }
}