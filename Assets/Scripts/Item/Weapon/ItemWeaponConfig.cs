using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Winter Universe/Item/New Weapon")]
    public class ItemWeaponConfig : ItemConfig
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _poise = 25f;
        [SerializeField] private float _attackSpeed = 1f;
        [SerializeField] private Vector2 _attackSize = Vector2.one;

        public float Damage => _damage;
        public float Poise => _poise;
        public float AttackSpeed => _attackSpeed;
        public Vector2 AttackSize => _attackSize;
    }
}