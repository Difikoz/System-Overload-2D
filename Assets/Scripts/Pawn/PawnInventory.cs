using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : MonoBehaviour
    {
        public Action<List<ItemConfig>> OnInventoryChanged;

        private PawnController _pawn;
        private float _currentWeight;
        private List<ItemConfig> _items = new();

        public float CurrentWeight => _currentWeight;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _items.Clear();
        }

        public void AddItem(ItemConfig item, int amount = 1)
        {
            while (amount > 0)
            {
                _items.Add(item);
                amount--;
            }
            UpdateInventory();
        }

        public void RemoveItem(ItemConfig item, int amount = 1)
        {
            while (amount > 0)
            {
                _items.Remove(item);
                amount--;
            }
            UpdateInventory();
        }

        public int AmountOfItem(ItemConfig item)
        {
            int amount = 0;
            foreach (ItemConfig currentItem in _items)
            {
                if (currentItem == item)
                {
                    amount++;
                }
            }
            return amount;
        }

        private void UpdateInventory()
        {
            float weight = 0f;
            foreach (ItemConfig item in _items)
            {
                weight += item.Weight;
            }
            _currentWeight = weight;
            OnInventoryChanged?.Invoke(_items);
        }
    }
}