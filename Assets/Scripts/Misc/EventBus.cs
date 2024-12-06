using System;
using UnityEngine;

namespace WinterUniverse
{
    public static class EventBus
    {
        public static Action<bool> OnGamePaused;
        //
        public static Action<int, int> OnTimeChanged;
        public static Action<int, int, int> OnDateChanged;
        //
        public static Action<int> OnStatusActiveWindowChanged;
        public static Action<ItemConfig> OnInventorySelectedItemChanged;

        public static void GamePaused(bool paused)
        {
            OnGamePaused?.Invoke(paused);
        }
        //
        public static void TimeChanged(int hour, int minute)
        {
            OnTimeChanged?.Invoke(hour, minute);
        }
        public static void DateChanged(int day, int month, int year)
        {
            OnDateChanged?.Invoke(day, month, year);
        }
        //
        public static void StatusActiveWindowChanged(int index)
        {
            OnStatusActiveWindowChanged?.Invoke(index);
        }

        public static void InventorySelectedItemChanged(ItemConfig item)
        {
            OnInventorySelectedItemChanged?.Invoke(item);
        }
    }
}