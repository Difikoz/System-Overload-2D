using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Winter Universe/Item/New Armor")]
    public class ItemArmorConfig : ItemConfig
    {
        [SerializeField, Range(0f, 100f)] private float _resistance = 5f;

        public float Resistance => _resistance;
    }
}