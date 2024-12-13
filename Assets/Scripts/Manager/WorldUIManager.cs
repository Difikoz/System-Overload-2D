using UnityEngine;

namespace WinterUniverse
{
    public class WorldUIManager : MonoBehaviour
    {
        private FadeScreenUI _fadeScreen;
        private StatusMenuUI _statusMenu;
        private TimeOfDayBarUI _timeOfDay;
        private InteractableBarUI _interactableBar;

        public FadeScreenUI FadeScreen => _fadeScreen;
        public StatusMenuUI StatusMenu => _statusMenu;

        public void Initialize()
        {
            _fadeScreen = GetComponentInChildren<FadeScreenUI>();
            _statusMenu = GetComponentInChildren<StatusMenuUI>();
            _timeOfDay = GetComponentInChildren<TimeOfDayBarUI>();
            _interactableBar = GetComponentInChildren<InteractableBarUI>();
            _statusMenu.Initialize();
            _timeOfDay.Initialize();
            _interactableBar.Initialize();
        }

        public void OnDespawn()
        {
            _statusMenu.OnDespawn();
            _timeOfDay.OnDespawn();
            _interactableBar.OnDespawn();
        }

        public void ToggleStatusMenu()
        {
            if (_statusMenu.isActiveAndEnabled)
            {
                CloseStatusMenu();
            }
            else
            {
                OpenStatusMenu();
            }
        }

        private void OpenStatusMenu()
        {
            _statusMenu.gameObject.SetActive(true);
            WorldManager.StaticInstance.TimeManager.PauseGame();
        }

        private void CloseStatusMenu()
        {
            WorldManager.StaticInstance.TimeManager.UnpauseGame();
            _statusMenu.gameObject.SetActive(false);
        }
    }
}