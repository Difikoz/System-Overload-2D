using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Winter Universe/Item/New Armor")]
    public class ItemArmorConfig : ItemConfig
    {
        [SerializeField, Range(0f, 100f)] private float _resistance = 5f;

        public float Resistance => _resistance;

        private void OnValidate()
        {
            _itemType = ItemType.Armor;
        }
    }
}