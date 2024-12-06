using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InteractableItem : InteractableBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private ItemConfig _testItem;

        private ItemConfig _config;

        private void Start()
        {
            Setup(_testItem);
        }

        public void Setup(ItemConfig config)
        {
            _config = config;
            _spriteRenderer.sprite = _config.LootSprite;
            _collider.SetPath(0, _spriteRenderer.sprite.vertices);
        }

        public override string GetText()
        {
            return $"Pickup [{_config.DisplayName}]";
        }

        public override void Interact(PawnController pawn)
        {
            pawn.PawnInventory.AddItem(_config);
            LeanPool.Despawn(gameObject);
        }
    }
}