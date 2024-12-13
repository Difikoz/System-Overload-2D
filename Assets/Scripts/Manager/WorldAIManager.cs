using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldAIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _AIPrefab;

        private List<AIController> _spawnedAI = new();
        private List<AIController> _aiToDespawn = new();
        private List<SpawnerAI> _spawners = new();

        public void Initialize()
        {
            SpawnerAI[] spawners = FindObjectsByType<SpawnerAI>(FindObjectsSortMode.None);
            if (spawners.Length > 0)
            {
                foreach (SpawnerAI spawner in spawners)
                {
                    _spawners.Add(spawner);
                    spawner.Initialize();
                }
            }
        }

        public AIController SpawnAI(Vector3 position)
        {
            AIController ai = LeanPool.Spawn(_AIPrefab, position, Quaternion.identity).GetComponent<AIController>();
            ai.Initialize();
            _spawnedAI.Add(ai);
            return ai;
        }

        public void DespawnAI(AIController ai)
        {
            if (_spawnedAI.Contains(ai) && !_aiToDespawn.Contains(ai))
            {
                _aiToDespawn.Add(ai);
            }
        }

        public void OnFixedUpdate()
        {
            if (_spawnedAI.Count > 0)
            {
                foreach (AIController ai in _spawnedAI)
                {
                    ai.OnFixedUpdate();
                }
            }
            if (WorldManager.StaticInstance.TimeManager.Paused)
            {
                return;
            }
            foreach (SpawnerAI spawner in _spawners)
            {
                spawner.OnFixedUpdate();
            }
            if (_aiToDespawn.Count > 0)
            {
                for (int i = _aiToDespawn.Count - 1; i >= 0; i--)
                {
                    _spawnedAI.Remove(_aiToDespawn[i]);
                    _aiToDespawn[i].Despawn();
                    _aiToDespawn.RemoveAt(i);
                }
            }
        }
    }
}