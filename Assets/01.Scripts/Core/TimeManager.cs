using System.Collections.Generic;
using UnityEngine;
namespace Core
{

    public static class TimeManager
    {
        private static readonly float _defaultTimeScale = 1f;
        private static Stack<float> _timeScaleRecord;

        public static void ResetTimeScale()
        {
            CheckRecord();
            Time.timeScale = _defaultTimeScale;
            _timeScaleRecord.Clear();
        }

        public static void AddTimeScaleRecord(float newTimeScale)
        {
            CheckRecord();
            _timeScaleRecord.Push(newTimeScale);
            Time.timeScale = newTimeScale;
        }

        public static void RemoveTimeScaleRecord()
        {
            CheckRecord();

            if (_timeScaleRecord.Count > 0)
                _timeScaleRecord.Pop();

            Time.timeScale = _timeScaleRecord.Count > 0 ? _timeScaleRecord.Peek() : 1f;
        }

        private static void CheckRecord()
        {
            if (_timeScaleRecord == null)
                _timeScaleRecord = new();
        }
    }
}