using System.IO;
using UnityEngine;

namespace Core.DataManage
{
    public static class DataManager
    {
        public static StageDataGroup stageDataGroup;
        public static UpgradeData upgradeData;
        public static GameSetting settingData;

        private static readonly string LocalPath = Application.dataPath + "/SaveData";

        private static readonly string StageDataFileName = "StageData.json";
        private static readonly string UpgradeDataFileName = "UpgradeData.json";
        private static readonly string GameSettingFileName = "GameSetting.json";

        private static bool _pathChecked = false;

        public static void Load()
        {
            stageDataGroup = LoadData<StageDataGroup>(StageDataFileName);
            upgradeData = LoadData<UpgradeData>(UpgradeDataFileName);
            settingData = LoadData<GameSetting>(GameSettingFileName);
        }

        public static void Save()
        {
            SaveData(stageDataGroup, StageDataFileName);
            SaveData(upgradeData, UpgradeDataFileName);
            SaveData(settingData, GameSettingFileName);
        }

        private static T LoadData<T>(string fileName) where T : new()
        {
            EnsureLocalPath();

            string path = Path.Combine(LocalPath, fileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                try
                {
                    return JsonUtility.FromJson<T>(json);
                }
                catch
                {
                    Debug.LogWarning($"[DataManager] Failed parsing to JSON : {fileName}");
                }
            }

            T newData = new T();
            SaveData(newData, fileName);
            return newData;
        }

        private static void SaveData<T>(T data, string fileName)
        {
            EnsureLocalPath();

            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(LocalPath, fileName);
            File.WriteAllText(path, json);
        }

        private static void EnsureLocalPath()
        {
            if (_pathChecked) return;

            if (!Directory.Exists(LocalPath))
            {
                Debug.Log("[DataManager] Generate SaveData Folder");
                Directory.CreateDirectory(LocalPath);
            }

            _pathChecked = true;
        }
    }
}
