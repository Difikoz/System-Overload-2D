using Lean.Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class InventoryMenuUI : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private int _windowIndex;
        [Header("Inventory")]
        [SerializeField] private Transform _inventoryContentRoot;
        [SerializeField] private GameObject _inventorySlotPrefab;
        [Header("Item Info")]
        [SerializeField] private Image _inventoryItemIconImage;
        [SerializeField] private TMP_Text _inventoryItemNameText;
        [SerializeField] private TMP_Text _inventoryItemDescriptionText;

        public void Initialize()
        {
            EventBus.OnInventorySelectedItemChanged += UpdateItemInfo;
            WorldManager.StaticInstance.Player.PawnInventory.OnInventoryChanged += UpdateInventory;
        }

        public void OnDespawn()
        {
            EventBus.OnInventorySelectedItemChanged -= UpdateItemInfo;
            WorldManager.StaticInstance.Player.PawnInventory.OnInventoryChanged -= UpdateInventory;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            EventBus.OnStatusActiveWindowChanged(_windowIndex);
        }

        private void UpdateItemInfo(ItemConfig item)
        {
            _inventoryItemIconImage.sprite = item.IconSprite;
            _inventoryItemNameText.text = item.DisplayName;
            _inventoryItemDescriptionText.text = item.Description;
        }

        private void UpdateInventory(List<ItemConfig> items)
        {
            while (_inventoryContentRoot.childCount > 0)
            {
                LeanPool.Despawn(_inventoryContentRoot.GetChild(0).gameObject);
            }
            if (items.Count > 0)
            {
                UpdateItemInfo(items[0]);
                foreach (ItemConfig item in items)
                {
                    LeanPool.Spawn(_inventorySlotPrefab, _inventoryContentRoot).GetComponent<InventorySlotUI>().Setup(item);
                }
            }
        }

    }
}