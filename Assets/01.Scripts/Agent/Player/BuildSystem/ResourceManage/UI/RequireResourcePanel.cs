using System.Collections.Generic;
using UIManage;
using UnityEngine;
namespace BuildSystem.ResourceManage.UI
{

    public class RequireResourcePanel : UIPanel
    {
        [SerializeField] private ResourceDataGroupSO _dataGroup;

        [SerializeField] private ResourceDisplayer _displayerPrefab;
        [SerializeField] private Transform _contentTrm;

        
        private Queue<ResourceDisplayer> _pool;
        private List<ResourceDisplayer> _enableSlots;
        [SerializeField] private int _initPoolAmount = 5;

        protected override void Awake()
        {
            base.Awake();

            _pool = new();
            _enableSlots = new();
            for (int i = 0; i < _initPoolAmount; i++)
            {
                ResourceDisplayer displayer = Instantiate(_displayerPrefab, _contentTrm);
                displayer.SetEnable(false, false);
                _pool.Enqueue(displayer);
            }
        }

        private void DisableAllDisplayers()
        {
            for (int i = 0; i < _enableSlots.Count; i++)
            {
                ResourceDisplayer displayer = _enableSlots[i];
                displayer.SetEnable(false);
                _pool.Enqueue(displayer);
            }
            _enableSlots.Clear();
        }

        public ResourceDisplayer GetDisplayer()
        {
            ResourceDisplayer displayer = _pool.Count > 0 ?
                _pool.Dequeue() : Instantiate(_displayerPrefab, _contentTrm);

            return displayer;
        }

        public void SetRequireData(ResourceData[] datas)
        {
            DisableAllDisplayers();
            for (int i = 0; i < datas.Length; i++)
            {
                _dataGroup.GetData(datas[i].type);
                ResourceDisplayer displayer = GetDisplayer();
                _enableSlots.Add(displayer);
                displayer.SetEnable(true);
                displayer.SetResourceRequireData(
                    _dataGroup.GetData(datas[i].type),
                    ResourceManager.Instance.GetAmount(datas[i].type),
                    datas[i].amount);

                
            }
        }
    }
}