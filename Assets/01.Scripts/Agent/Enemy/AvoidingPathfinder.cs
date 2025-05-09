using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agents.Enemies.AI.PathFinder
{

    public class AvoidingPathfinder : MonoBehaviour
    {
        [Header("Detection Settings")]
        public float maxDetectDistance = 10f; // 감지용 거리 (충분히 길게 설정)
        [Header("Path Settings")]
        public float stepDistance = 0.5f;
        [SerializeField] float _avoidOffset = 0.7f;
        [SerializeField] private float _castRadius = 0.3f;
        public int maxDepth = 100;
        public LayerMask bulletMask;

        [Header("Debug Settings")]
        public Transform _startPointTrm;
        public Transform _endPointTrm;

        private Stack<Vector2> pathStack = new();
        private HashSet<Vector2> visited = new();

        /// <summary>
        /// DFS + 우회 회피 경로 탐색
        /// </summary>
        private bool FindPathRecursive(Vector2 current, Vector2 target, int depth, float stepDistance, Vector2? lastDirection = null)
        {
            if (depth > maxDepth) return false;

            Vector2Int gridKey = Vector2Int.RoundToInt(current * 10f);
            if (visited.Contains(gridKey)) return false;
            visited.Add(gridKey);

            if (Vector2.Distance(current, target) < stepDistance)
            {
                pathStack.Push(target);
                return true;
            }

            Vector2 direction = (target - current).normalized;
            RaycastHit2D hit = Physics2D.CircleCast(current, _castRadius, direction, stepDistance, bulletMask);

            if (!hit)
            {
                Vector2 next = current + direction * stepDistance;

                if (FindPathRecursive(next, target, depth + 1, stepDistance, direction))
                {
                    // 직전 방향과 지금 방향이 다르면 꺾인 지점 -> 기록
                    if (lastDirection == null || Vector2.Angle(direction, lastDirection.Value) > 1f)
                    {
                        pathStack.Push(current);
                    }
                    return true;
                }
            }
            else
            {
                // Vector2 normal = hit.normal;
                // Vector2 perp = Vector2.Perpendicular(normal).normalized;
                // Vector2 rightAvoid = current + perp * avoidOffset + direction * stepDistance;
                // Vector2 leftAvoid = current - perp * avoidOffset + direction * stepDistance;
                Vector2 perp = Vector2.Perpendicular(direction).normalized;
                Vector2 rightAvoid = current + perp * _avoidOffset + direction * stepDistance;
                Vector2 leftAvoid = current - perp * _avoidOffset + direction * stepDistance;

                Vector2 first = (Vector2.Distance(rightAvoid, target) < Vector2.Distance(leftAvoid, target)) ? rightAvoid : leftAvoid;
                Vector2 second = (first == rightAvoid) ? leftAvoid : rightAvoid;

                if (FindPathRecursive(first, target, depth + 1, stepDistance * 0.9f, direction))
                {
                    pathStack.Push(current); // 회피 시도 지점 -> 무조건 기록
                    return true;
                }
                if (FindPathRecursive(second, target, depth + 1, stepDistance * 0.9f, direction))
                {
                    pathStack.Push(current); // 회피 시도 지점 -> 무조건 기록
                    return true;
                }
            }

            return false;
        }

        public Vector2[] FindPath(Vector2 start, Vector2 target)
        {
            pathStack.Clear();
            visited.Clear();

            // 시작 단계에서는 최대 거리 (예: 1f)로 시작하고 점점 짧아지게 처리
            bool success = FindPathRecursive(start, target, 0, 1f);  // 초기 stepDistance 1f로 시작
            if (!success)
            {
                Debug.LogWarning("❌ 경로 탐색 실패: 피할 수 없는 탄막 또는 막힘");
                return null;
            }

            return pathStack.Reverse().ToArray();
        }

        private void LateUpdate()
        {
            DebugFindPath();
        }

        [ContextMenu("DebugFind")]
        private void DebugFindPath()
        {
            Vector2[] points = FindPath(_startPointTrm.position, _endPointTrm.position);

            if (points == null || points.Length < 2)
            {
                Debug.LogWarning("❌ 경로 없음 — 피할 수 없는 상황 또는 오류");
                return;
            }

            for (int i = 1; i < points.Length; i++)
            {
                print(points[i - 1]);
                Debug.DrawLine(points[i - 1], points[i], Color.red, Time.deltaTime);
            }

            Debug.Log($"✅ 경로 시각화 완료: {points.Length} 포인트");
        }

    }

}