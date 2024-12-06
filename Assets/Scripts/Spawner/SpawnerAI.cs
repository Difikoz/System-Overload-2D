using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnerAI : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new();
        [SerializeField] private int _spawnCount = 2;
        [SerializeField] private int _maxCount = 4;
        [SerializeField] private float _cooldown = 30f;

        private List<AIController> _spawnedAI = new();
        private int _pointIndex;

        public void Initialize()
        {
            _pointIndex = 0;
            StartCoroutine(SpawnTimer());
        }

        private IEnumerator SpawnTimer()
        {
            WaitForSeconds delay = new(_cooldown / 2f);
            while (true)
            {
                for (int i = 0; i < _spawnCount; i++)
                {
                    if (_spawnedAI.Count - 1 == _maxCount)
                    {
                        break;
                    }
                    Spawn();
                    yield return null;
                }
                yield return delay;
                if (_spawnedAI.Count > 0)// remove corpses
                {
                    for (int i = _spawnedAI.Count - 1; i >= 0; i--)
                    {
                        if (_spawnedAI[i].IsDead)
                        {
                            WorldManager.StaticInstance.AIManager.DespawnAI(_spawnedAI[i]);
                            _spawnedAI.RemoveAt(i);
                        }
                    }
                }
                yield return delay;
            }
        }

        private void Spawn()
        {
            _spawnedAI.Add(WorldManager.StaticInstance.AIManager.SpawnAI(_spawnPoints[_pointIndex].position));
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