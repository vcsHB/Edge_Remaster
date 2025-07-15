using UnityEngine;
namespace Core.DataManage
{

    public class StageDataGroup
    {
        public bool isTutorialCleared;
        public int enterStageId;
        public StageData[] datas;

        public StageDataGroup()
        {
            datas = new StageData[3]
            {
                new StageData()
                {
                    id = 0,
                    isUnlocked = true
                },
                new StageData()
                {
                    id = 1,
                    isUnlocked = false
                },
                new StageData()
                {
                    id = 2,
                    isUnlocked = false
                }
            };
        }


    }
}