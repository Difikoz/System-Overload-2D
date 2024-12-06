using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InteractableItem : InteractableBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PolygonCollider2D _collider;

        private ItemConfig _config;
        private List<Vector2> _vertices = new();

        public void Setup(ItemConfig config)
        {
            _vertices.Clear();
            _config = config;
            _spriteRenderer.sprite = _config.LootSprite;
            _spriteRenderer.sprite.GetPhysicsShape(0, _vertices);
            _collider.SetPath(0, _vertices);
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