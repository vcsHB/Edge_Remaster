using System;
using System.Collections.Generic;
using Core.DataManage;
using UnityEditor;
using UnityEngine;
using UpgradeSystem;
namespace UIManage.TitleScene
{

    public class UpgradeInstallPanel : UIPanel
    {
        [SerializeField] private UpgradeDataGroupSO _dataGroup;
        [SerializeField] private UpgradeSlot _upgradeSlotPrefab;
        [SerializeField] private Transform _contentTrm;
        [SerializeField] private UpgradeDetailPanel _detailPanel;

        private Queue<UpgradeSlot> _pool = new();
        private List<UpgradeSlot> _enableSlots = new();
        private UpgradeData _upgradeSaveData;

        protected override void Awake()
        {
            DataManager.Load();
            _upgradeSaveData = DataManager.upgradeData;
            base.Awake();
            _detailPanel.OnUpgradeInstallEvent += HandleInstall;
        }

        private void HandleInstall(UpgradeDataSO data)
        {
            // Already checked conditions
            _upgradeSaveData.vertexCrystal -= data.requireVertexCost;
            _upgradeSaveData.metaCrystal -= data.requireMetaCost;
            _upgradeSaveData.unlockDatas.Add(data.id);
            RefreshUpgrades();
            DataManager.Save();
        }

        public override void Open()
        {
            base.Open();
            RefreshUpgrades();
        }

        [ContextMenu("RefreshUpgrades")]
        public void RefreshUpgrades()
        {
            ResetSlots();

            var unlocked = DataManager.upgradeData.unlockDatas;
            HashSet<int> created = new();

            foreach (int id in unlocked)
            {
                UpgradeDataSO data = _dataGroup.GetData(id);
                if (data == null || created.Contains(data.id)) continue;

                CreateSlotRecursive(data, true, created);
            }
        }

        private void CreateSlotRecursive(UpgradeDataSO data, bool isUnlocked, HashSet<int> created)
        {
            if (created.Contains(data.id)) return;

            // 슬롯 생성
            var slot = GetSlot();
            slot.SetData(data, isUnlocked);
            slot.SetEnable(true);
            created.Add(data.id);

            // 다음 단계까지만 (한 번만) 순회
            if (!isUnlocked) return;

            if (data.unlockList != null)
            {
                foreach (var next in data.unlockList)
                {
                    if (next != null)
                    {
                        bool nextUnlocked = DataManager.upgradeData.unlockDatas.Contains(next.id);
                        CreateSlotRecursive(next, nextUnlocked, created);
                    }
                }
            }
        }



        public void HandleUpgradeSelected(UpgradeDataSO upgradeData)
        {
            _detailPanel.SetData(
                upgradeData,
                DataManager.upgradeData.vertexCrystal,
                DataManager.upgradeData.metaCrystal);
        }

        private void ResetSlots()
        {
            for (int i = 0; i < _enableSlots.Count; i++)
            {
                _enableSlots[i].SetEnable(false);
                _enableSlots[i].OnUpgradeSelectEvent -= HandleUpgradeSelected;
            }
            _enableSlots.Clear();
        }

        private UpgradeSlot GetSlot()
        {
            UpgradeSlot newSlot = _pool.Count > 0 ?
            _pool.Dequeue() : Instantiate(_upgradeSlotPrefab, _contentTrm);
            _enableSlots.Add(newSlot);
            newSlot.OnUpgradeSelectEvent += HandleUpgradeSelected;

            return newSlot;
        }
    }
}