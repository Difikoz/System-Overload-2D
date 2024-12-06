using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StatusMenuUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _pages = new();

        private int _currentWindowIndex;
        private InventoryMenuUI _inventoryMenu;

        public InventoryMenuUI InventoryMenu => _inventoryMenu;

        public void Initialize()
        {
            EventBus.OnStatusActiveWindowChanged += OnActiveWindowChanged;
            _inventoryMenu = GetComponentInChildren<InventoryMenuUI>();
            _inventoryMenu.Initialize();
            gameObject.SetActive(false);
        }

        public void OnDespawn()
        {
            EventBus.OnStatusActiveWindowChanged -= OnActiveWindowChanged;
            _inventoryMenu.OnDespawn();
        }

        private void OnActiveWindowChanged(int index)
        {
            _pages[_currentWindowIndex].SetActive(false);
            _currentWindowIndex = index;
            _pages[_currentWindowIndex].SetActive(true);
        }
    }
}