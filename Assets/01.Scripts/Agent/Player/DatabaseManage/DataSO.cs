using Core.attribute;
using UnityEngine;
namespace BuildSystem.DataManage
{

    public class DataSO : ScriptableObject
    {
        [ReadOnly] public int id;
        public Sprite dataIconSprite;
        public string dataName;

        [TextArea] public string dataDescription;

        public DataCategory[] categorys;


        internal void SetID(int newID)
        {
            id = newID;
        }


    }
}