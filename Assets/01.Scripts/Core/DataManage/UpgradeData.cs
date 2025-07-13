using System.Collections.Generic;
using UnityEngine;

namespace Core.DataManage
{
    public class UpgradeData
    {
        public int vertexCrystal;
        public int metaCrystal;
        public List<int> unlockDatas;

        public UpgradeData()
        {
            unlockDatas = new List<int> { 0 };
        }
    }
}