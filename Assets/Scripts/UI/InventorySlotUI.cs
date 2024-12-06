using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class InventorySlotUI : MonoBehaviour, ISubmitHandler, IPointerClickHandler, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;

        private ItemConfig _item;

        public void Setup(ItemConfig item)
        {
            _item = item;
            _iconImage.sprite = _item.IconSprite;
            _nameText.text = _item.DisplayName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _item.OnUse(WorldManager.StaticInstance.Player);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            EventBus.InventorySelectedItemChanged(_item);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _item.OnUse(WorldManager.StaticInstance.Player);
        }
    }
}