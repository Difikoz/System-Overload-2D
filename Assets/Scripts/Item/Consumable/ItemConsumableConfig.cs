using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Winter Universe/Item/New Consumable")]
    public class ItemConsumableConfig : ItemConfig
    {
        [SerializeField] private List<string> _effects = new();
    }
}