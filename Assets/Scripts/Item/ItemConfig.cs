using UnityEngine;

namespace WinterUniverse
{
    public abstract class ItemConfig : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)] protected float _chanceToSpawn = 0.5f;
        [SerializeField] protected string _displayName = "Name";
        [SerializeField, TextArea] protected string _description = "Description";
        [SerializeField] protected ItemType _itemType;
        [SerializeField] protected Sprite _iconSprite;
        [SerializeField] protected Sprite _lootSprite;
        [SerializeField] protected float _weight = 1f;

        public string DisplayName => _displayName;
        public string Description => _description;
        public ItemType ItemType => _itemType;
        public Sprite IconSprite => _iconSprite;
        public Sprite LootSprite => _lootSprite;
        public float Weight => _weight;

        public virtual void OnUse(PawnController pawn)
        {

        }
    }
}