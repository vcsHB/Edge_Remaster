using System.Collections.Generic;
using System.Linq;
using SkillSystem;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Core;

namespace UI.InGame
{

    public class LevelUpPanel : MonoBehaviour, IWindowPanel
    {
        [Header("Panel Transition Setting")]
        [SerializeField] private float _activeDuration;
        [SerializeField] private float _activeHeight;
        [SerializeField] private bool _useUnscaledTime;


        [Header("Upgrade Content Setting")]
        [SerializeField] private RectTransform _contentTrm;
        [SerializeField] private UpgradeContentSlot _slotPrefab;
        [SerializeField] private List<UpgradeContentSlot> _slots;
        [SerializeField] private PowerUpListSO _powerUpList;
        [SerializeField] private float _closeDelay;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTrm;


        protected void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTrm = transform as RectTransform;
            _slots = _contentTrm.GetComponentsInChildren<UpgradeContentSlot>().ToList();
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].Initialize(i);
                _slots[i].OnSelectedEvent += HandleSelected;
            }
        }

        private void HandleSelected(int id)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetSelect(i == id);
            }
            Close();
        }

        public void HandleUpgrade()
        {

        }

        public void Open()
        {
            _canvasGroup.alpha = 1f;
            TimeManager.AddTimeScaleRecord(0f);
            SetPanelInteractable(true);
            _rectTrm.DOSizeDelta(
                new Vector2(_rectTrm.sizeDelta.x, _activeHeight), _activeDuration)
                .SetUpdate(_useUnscaledTime);
            SetUpPowerUpCards(3);
        }


        public void Close()
        {
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration)
                .SetEase(Ease.InExpo)
                .SetUpdate(_useUnscaledTime)
                .OnComplete(() =>
                {
                    SetPanelInteractable(false);
                    _canvasGroup.alpha = 0f;
                    TimeManager.RemoveTimeScaleRecord();

                });
        }

        private void SetUpPowerUpCards(int amount)
        {
            PowerUpSO[] arr = _powerUpList.list.Where(x => x.CheckCanUpgrade()).ToArray();

            if (arr.Length < amount)
            {
                Debug.LogError("Error!, Must Have 3 Item at Least");
                return;
            }
            if (_slots.Count <= amount)
            {
                int lackAmount = amount - _slots.Count;
                for (int i = 0; i < lackAmount; i++)
                {
                    UpgradeContentSlot newSlot = Instantiate(_slotPrefab, _contentTrm);
                    newSlot.OnSelectedEvent += HandleSelected;
                    newSlot.Initialize(_slots.Count);
                    _slots.Add(newSlot);
                }
            }

            for (int i = 0; i < _slots.Count; i++)
            {
                if (i < amount)
                {
                    _slots[i].SetEnable(true);
                    int index = Random.Range(0, arr.Length - i);
                    _slots[i].SetUpgradeInfo(arr[index]);
                    arr[index] = arr[arr.Length - 1 - i];
                }
                else
                    _slots[i].SetEnable(false);
            }

        }

        private void SetPanelInteractable(bool value)
        {
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }

    }

}