using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldManager : Singleton<WorldManager>
    {
        private bool _initialized;
        private PlayerController _player;
        private WorldAIManager _AIManager;
        private WorldAudioManager _audioManager;
        private WorldCameraManager _cameraManager;
        private WorldLayerManager _layerManager;
        private WorldTimeManager _timeManager;
        private WorldUIManager _UIManager;

        public PlayerController Player => _player;
        public WorldAIManager AIManager => _AIManager;
        public WorldAudioManager AudioManager => _audioManager;
        public WorldCameraManager CameraManager => _cameraManager;
        public WorldLayerManager LayerManager => _layerManager;
        public WorldTimeManager TimeManager => _timeManager;
        public WorldUIManager UIManager => _UIManager;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(Initialization());
        }

        private IEnumerator Initialization()
        {
            yield return null;
            _player = FindFirstObjectByType<PlayerController>();
            _AIManager = GetComponentInChildren<WorldAIManager>();
            _audioManager = GetComponentInChildren<WorldAudioManager>();
            _cameraManager = GetComponentInChildren<WorldCameraManager>();
            _layerManager = GetComponentInChildren<WorldLayerManager>();
            _timeManager = GetComponentInChildren<WorldTimeManager>();
            _UIManager = GetComponentInChildren<WorldUIManager>();
            yield return null;
            _player.Initialize();
            _AIManager.Initialize();
            _audioManager.Initialize();
            _timeManager.Initialize();
            _UIManager.Initialize();
            yield return null;
            _initialized = true;
            _timeManager.UnpauseGame();
        }

        private void FixedUpdate()
        {
            if (!_initialized)
            {
                return;
            }
            if (_timeManager.Paused)
            {
                return;
            }
            _player.OnFixedUpdate();
            _AIManager.OnFixedUpdate();
            _timeManager.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            if (!_initialized)
            {
                return;
            }
            if (_timeManager.Paused)
            {
                return;
            }
            _cameraManager.OnLateUpdate();
        }

        private void OnDestroy()
        {
            if (!_initialized)
            {
                return;
            }
            _initialized = false;
            _player.Despawn();
            _audioManager.StopAmbient();
        }
    }
}