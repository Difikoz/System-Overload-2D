using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldItemManager : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private List<ItemWeaponConfig> _weapons = new();
        [SerializeField] private List<ItemArmorConfig> _armors = new();
        [SerializeField] private List<ItemConsumableConfig> _consumables = new();
        [SerializeField] private List<ItemResourceConfig> _resources = new();

        private List<ItemConfig> _items = new();
        private List<SpawnerItem> _spawners = new();

        public void Initialize()
        {
            _items.Clear();
            _spawners.Clear();
            foreach (ItemWeaponConfig item in _weapons)
            {
                _items.Add(item);
            }
            foreach (ItemArmorConfig item in _armors)
            {
                _items.Add(item);
            }
            foreach (ItemConsumableConfig item in _consumables)
            {
                _items.Add(item);
            }
            foreach (ItemResourceConfig item in _resources)
            {
                _items.Add(item);
            }
            SpawnerItem[] spawners = FindObjectsByType<SpawnerItem>(FindObjectsSortMode.None);
            if (spawners.Length > 0)
            {
                foreach (SpawnerItem spawner in spawners)
                {
                    _spawners.Add(spawner);
                    spawner.Initialize();
                }
            }
        }

        public void OnFixedUpdate()
        {
            if (WorldManager.StaticInstance.TimeManager.Paused)
            {
                return;
            }
            foreach (SpawnerItem spawner in _spawners)
            {
                spawner.OnFixedUpdate();
            }
        }

        public InteractableItem SpawnItem(ItemConfig config, Vector3 position)
        {
            InteractableItem item = LeanPool.Spawn(_itemPrefab, position, Quaternion.identity).GetComponent<InteractableItem>();
            item.Setup(config);
            return item;
        }

        public ItemConfig GetItem(string name)
        {
            foreach (ItemConfig item in _items)
            {
                if (item.DisplayName == name)
                {
                    return item;
                }
            }
            return null;
        }
    }
}