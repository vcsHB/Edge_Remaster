using System.Collections.Generic;
using BuildSystem.Structures;
using UnityEngine;

using Slot = BuildSystem.StructureUpgradeSlot;

namespace BuildSystem.SelectorManage
{

    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private Slot _structureUpgradeSlotPrefab;

        private Queue<Slot> _pool;
        private List<Slot> _enabledSlots;
        private int _currentUpgradeVariationAmount;
        private int _currentSelectedSlotIndex;


        private void Awake()
        {
            _pool = new();

        }

        public void SelectSlot(int index)
        {
            if (_currentSelectedSlotIndex > -1)
                _enabledSlots[_currentSelectedSlotIndex].SetSelect(false);
            _enabledSlots[index].SetSelect(true);
            _currentSelectedSlotIndex = index;
        }



        private void ResetSlots()
        {
            for (int i = 0; i < _enabledSlots.Count; i++)
            {
                _enabledSlots[i].SetEnable(false);
                _enabledSlots[i].SetSelect(false);
                _pool.Enqueue(_enabledSlots[i]);
            }
        }
        private Slot GetSlot()
        {
            Slot slot = _pool.Count > 0 ?
            _pool.Dequeue() : Instantiate(_structureUpgradeSlotPrefab);
            _enabledSlots.Add(slot);
            slot.SetEnable(true);
            return slot;
        }

        public void SetUpgradeSlots(StructureDataSO[] structureDatas)
        {
            ResetSlots();
            _currentUpgradeVariationAmount = structureDatas.Length;
            for (int i = 0; i < _currentUpgradeVariationAmount; i++)
            {
                Slot slot = GetSlot();
                slot.SetData(structureDatas[i]);
            }
            _currentSelectedSlotIndex = 0;


        }
    }
}