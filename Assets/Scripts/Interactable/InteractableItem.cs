using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InteractableItem : InteractableBase
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PolygonCollider2D _collider;

        private ItemConfig _item;
        private List<Vector2> _vertices = new();

        public void Setup(ItemConfig item)
        {
            _vertices.Clear();
            _item = item;
            _rb.mass = _item.Weight;
            _spriteRenderer.sprite = _item.LootSprite;
            _spriteRenderer.sprite.GetPhysicsShape(0, _vertices);
            _collider.SetPath(0, _vertices);
        }

        public override string GetText()
        {
            return $"Pickup [{_item.DisplayName}]";
        }

        public override void Interact(PawnController pawn)
        {
            pawn.PawnInventory.AddItem(_item);
            LeanPool.Despawn(gameObject);
        }
    }
}