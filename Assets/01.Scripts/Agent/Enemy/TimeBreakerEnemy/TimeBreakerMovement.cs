using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
namespace Agents.Enemies
{

    public class TimeBreakerMovement : MonoBehaviour
    {
        [Header("TimeBreak MoveAbility Settings")]
        [SerializeField] private float _moveDuration = 0.15f;
        [SerializeField] private float _moveTermDuration = 0.1f;
        [SerializeField] private Ease _movementEase;


        public void MoveToPoints(Vector2[] points, Action OnCompleteEvent = null)
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < points.Length; i++)
            {
                sequence.Append(transform.DOMove(points[i], _moveDuration).SetEase(_movementEase));
                sequence.AppendInterval(_moveTermDuration);
            }
            sequence.OnComplete(() => OnCompleteEvent?.Invoke());
        }

        

    }
}