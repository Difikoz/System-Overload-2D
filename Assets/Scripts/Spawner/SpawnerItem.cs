using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnerItem : MonoBehaviour
    {
        [SerializeField] private List<ItemConfig> _items = new();
        [SerializeField] private List<Transform> _spawnPoints = new();
        [SerializeField] private int _spawnCount = 1;
        [SerializeField] private int _maxCount = 2;
        [SerializeField] private float _spawnCooldown = 30f;
        [SerializeField] private float _clearCooldown = 10f;

        private List<InteractableItem> _spawnedItems = new();
        private int _pointIndex;
        private float _spawnTime;
        private float _clearTime;

        public void Initialize()
        {
            _pointIndex = 0;
            _spawnTime = _spawnCooldown;
        }

        public void OnFixedUpdate()
        {
            _clearTime += Time.fixedDeltaTime;
            if (_clearTime >= _clearCooldown)
            {
                if (_spawnedItems.Count > 0)// remove inactive
                {
                    for (int i = _spawnedItems.Count - 1; i >= 0; i--)
                    {
                        if (!_spawnedItems[i].isActiveAndEnabled)
                        {
                            _spawnedItems.RemoveAt(i);
                        }
                    }
                }
                _clearTime = 0f;
                return;
            }
            _spawnTime += Time.fixedDeltaTime;
            if (_spawnTime >= _spawnCooldown)
            {
                for (int i = 0; i < _spawnCount; i++)
                {
                    if (_spawnedItems.Count - 1 == _maxCount)
                    {
                        break;
                    }
                    Spawn();
                }
                _spawnTime = 0f;
            }
        }

        private void Spawn()
        {
            _spawnedItems.Add(WorldManager.StaticInstance.ItemManager.SpawnItem(_items[Random.Range(0, _items.Count)], _spawnPoints[_pointIndex].position));
            if (_pointIndex == _spawnPoints.Count - 1)
            {
                _pointIndex = 0;
            }
            else
            {
                _pointIndex++;
            }
        }
    }
}