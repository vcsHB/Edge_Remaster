using Agents;
using TMPro;
using UnityEngine;
namespace ObjectManage
{

    public class MovePoint : MonoBehaviour
    {
        private Collider2D _collider;
        [SerializeField] private bool _isActive = true;
        [SerializeField] private TextMeshPro _leftTimeText;
        public bool IsActive => _isActive;
        private Health _healthCompo;
        private MovePointRenderer _renderer;

        private float _currentTime = 0f;
        [Header("Tower Setting")]
        [SerializeField] private float _reviveCooltime = 30f;
        [SerializeField] private float _reviveTimeMultiple = 1.8f;
        [SerializeField] private float _maxHealth;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _healthCompo = GetComponent<Health>();
            _renderer = transform.Find("Visual").GetComponent<MovePointRenderer>();

            _healthCompo.OnDieEvent.AddListener(HandleDestroyEvent);
            _healthCompo.Initialize(_maxHealth);
        }

        private void Update()
        {
            if(_isActive) return;
            _currentTime += Time.deltaTime; 
            UpdateText();
            if(_currentTime >= _reviveCooltime)
            {
                HandleRevive();
            }
        }

        private void HandleRevive()
        {
            _isActive = true;
            _renderer.SetActive(false);
            _collider.enabled = true;
            _healthCompo.SetMaxHealth();
            _leftTimeText.gameObject.SetActive(false);
        }

        private void HandleDestroyEvent()
        {
            _isActive = false;
            _renderer.SetActive(false);
            _collider.enabled = false;
            _leftTimeText.gameObject.SetActive(true);
        }

        private void UpdateText()
        {
            int leftSecond = (int)(_reviveCooltime - _currentTime);
            _leftTimeText.text = $"{leftSecond}";
        }

        public void Enter()
        {
            //_collider.enabled = false;
            //_healthCompo.isResist = false;
        }

        public void Exit()
        {
            //_collider.enabled = true;
            //_healthCompo.isResist = true;
        }


    }
}