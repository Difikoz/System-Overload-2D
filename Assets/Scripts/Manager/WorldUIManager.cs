using UnityEngine;

namespace WinterUniverse
{
    public class WorldUIManager : MonoBehaviour
    {
        private StatusMenuUI _statusMenu;
        private TimeOfDayBarUI _timeOfDay;
        private InteractableBarUI _interactableBar;

        public StatusMenuUI StatusMenu => _statusMenu;

        public void Initialize()
        {
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
            //WorldManager.StaticInstance.TimeManager.PauseGame();
            _statusMenu.gameObject.SetActive(true);
        }

        private void CloseStatusMenu()
        {
            _statusMenu.gameObject.SetActive(false);
            //WorldManager.StaticInstance.TimeManager.UnpauseGame();
        }
    }
}