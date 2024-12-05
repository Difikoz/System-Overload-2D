using System;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldTimeManager : MonoBehaviour
    {
        public Action<int, int> OnTimeChanged;
        public Action<int, int, int> OnDateChanged;

        private bool _paused = true;

        [SerializeField] private float _timeScaleMultiplier = 600f;
        [SerializeField] private float _timeScale = 1f;

        [SerializeField] private float _secondsInMinute = 60f;
        [SerializeField] private int _minutesInHour = 60;
        [SerializeField] private int _hoursInDay = 24;
        [SerializeField] private int _daysInMonth = 30;
        [SerializeField] private int _monthsInYear = 12;

        [SerializeField] private int _startingMinute = 24;
        [SerializeField] private int _startingHour = 9;
        [SerializeField] private int _startingDay = 12;
        [SerializeField] private int _startingMonth = 5;
        [SerializeField] private int _startingYear = 3059;

        private float _second;
        private int _minute;
        private int _hour;
        private int _day;
        private int _month;
        private int _year;

        public float TimeScale => _timeScale;
        public float Second => _second;
        public int Minute => _minute;
        public int Hour => _hour;
        public int Day => _day;
        public int Month => _month;
        public int Year => _year;
        public bool Paused => _paused;

        public void Initialize()
        {
            _minute = _startingMinute;
            _hour = _startingHour;
            _day = _startingDay;
            _month = _startingMonth;
            _year = _startingYear;
        }

        public void OnFixedUpdate()
        {
            _second += TimeScale * _timeScaleMultiplier * Time.fixedDeltaTime;
            if (_second >= _secondsInMinute)
            {
                _second -= _secondsInMinute;
                AddMinute();
            }
        }

        public void ChangeTimeScale(float timeScale = 1f)
        {
            _timeScale = timeScale;
        }

        public void PauseGame()
        {
            _paused = true;
            // other logic
        }

        public void UnpauseGame()
        {
            _paused = false;
            // other logic
        }

        public void AddMinute(int amount = 1)
        {
            _minute += amount;
            while (_minute >= _minutesInHour)
            {
                _minute -= _minutesInHour;
                AddHour();
            }
            OnTimeChanged?.Invoke(_hour, _minute);
        }

        public void AddHour(int amount = 1)
        {
            _hour += amount;
            while (_hour >= _hoursInDay)
            {
                _hour -= _hoursInDay;
                AddDay();
            }
            OnTimeChanged?.Invoke(_hour, _minute);
        }

        public void AddDay(int amount = 1)
        {
            _day += amount;
            while (_day > _daysInMonth)
            {
                _day -= _daysInMonth;
                AddMonth();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddMonth(int amount = 1)
        {
            _month += amount;
            while (_month > _monthsInYear)
            {
                _month -= _monthsInYear;
                AddYear();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddYear(int amount = 1)
        {
            _year += amount;
            OnDateChanged?.Invoke(_day, _month, _year);
        }
    }
}