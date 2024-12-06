using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class TimeOfDayBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject _barWindow;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _dateText;

        public void Initialize()
        {
            EventBus.OnTimeChanged += UpdateTime;
            EventBus.OnDateChanged += UpdateDate;
        }

        public void OnDespawn()
        {
            EventBus.OnTimeChanged -= UpdateTime;
            EventBus.OnDateChanged -= UpdateDate;
        }

        public void ShowWindow()
        {
            _barWindow.SetActive(true);
        }

        public void HideWindow()
        {
            _barWindow.SetActive(false);
        }

        private void UpdateTime(int hour, int minute)
        {
            _timeText.text = $"{hour:00}:{minute:00}";
        }

        private void UpdateDate(int day, int month, int year)
        {
            _dateText.text = $"{day:00}.{month:00}.{year:0000}";
        }
    }
}