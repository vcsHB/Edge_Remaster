using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
namespace Agents.Enemies
{

    public class TimeBreakerMovement : EnemyMovement
    {
        [SerializeField] private bool _useUnscaledTime = true;

        public void MoveToPoints(Vector2[] points, float moveDuration, float moveTermDuration, Ease movementEase = Ease.Linear, Action OnCompleteEvent = null)
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < points.Length; i++)
            {
                sequence.Append(transform.DOMove(points[i], moveDuration).SetEase(movementEase));
                sequence.AppendInterval(moveTermDuration);
            }
            sequence.SetUpdate(_useUnscaledTime);
            sequence.OnComplete(() => OnCompleteEvent?.Invoke());
        }



    }
}