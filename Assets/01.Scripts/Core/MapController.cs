using ObjectManage;
using UnityEngine;

namespace Core.MapConrtrolSystem
{

    public class MapController : MonoBehaviour
    {
        [SerializeField] private MovePoint[] _points;
        [SerializeField] private LineRenderer _railRenderer;
        [SerializeField] private Vector2[] _offsets;

        [Header("Clamping Setting")]
        [SerializeField] private Transform _topClamper;
        [SerializeField] private Transform _leftClamper;
        [SerializeField] private Transform _rightClamper;
        [SerializeField] private Transform _bottomClamper;

        private void Awake()
        {
            Camera mainCamera = Camera.main;

            // 스크린 모서리의 뷰포트 좌표
            Vector2 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
            Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector2 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

            //  1       2
            //
            //  4       3

            Vector2 pos = topLeft + _offsets[0];
            SetAnchor(0, pos);

            pos = topRight + _offsets[1];
            SetAnchor(1, pos);

            pos = bottomRight + _offsets[2];
            SetAnchor(2, pos);

            pos = bottomLeft + _offsets[3];
            SetAnchor(3, pos);

            Debug.Log($"Top Left: {topLeft}");
            Debug.Log($"Top Right: {topRight}");
            Debug.Log($"Bottom Left: {bottomLeft}");
            Debug.Log($"Bottom Right: {bottomRight}");

            _topClamper.position = new Vector2(0, topLeft.y);
            _leftClamper.position = new Vector2(topLeft.x, 0);
            _rightClamper.position = new Vector2(bottomRight.x, 0);
            _bottomClamper.position = new Vector2(0, bottomLeft.y);
        }

        public void SetAnchor(int index, Vector2 position)
        {
            _points[index].transform.position = position;
            _railRenderer.SetPosition(index, position);

        }

        public Vector2 GetRandomPointPosition()
        {
            return _points[Random.Range(0, _points.Length)].transform.position;
        }

    }

}