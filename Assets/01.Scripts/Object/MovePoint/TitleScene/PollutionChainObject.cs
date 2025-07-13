using UnityEngine;
namespace ObjectManage
{

    public class PollutionChainObject : MonoBehaviour
    {
        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}