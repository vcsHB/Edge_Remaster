using System;
using BuildSystem;
using BuildSystem.Structures;
using Core.EventSystem;
using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class DestroyStep : TutorialStep
    {
        [SerializeField] private GameEventChannelSO _buildEventChannel;

        [SerializeField] private StructureDataSO _buildTarget;

        private void Awake()
        {
            if (_buildEventChannel == null)
            {
                Debug.LogError("Build Event Channel is Not binded");
                return;
            }
            _buildEventChannel.AddListener<DestroyData>(HandleDestroy);
            // 스텝에 들어온 시점에. 해당 위치에 이미 설치된 것에 대한 예외 처리 필요
            // todo. 
        }

        private void HandleDestroy(DestroyData data)
        {
            if (data is DestroyData destroyData)
            {
                if (destroyData.data.Id == _buildTarget.Id)
                {
                    Exit();
                }
            }
        }
    }
}