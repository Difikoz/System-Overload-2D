using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Winter Universe/Item/New Consumable")]
    public class ItemConsumableConfig : ItemConfig
    {
        [SerializeField] private List<string> _effects = new();

        private void OnValidate()
        {
            _itemType = ItemType.Consumable;
        }
    }
}