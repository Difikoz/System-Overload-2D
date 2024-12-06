using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInteraction : MonoBehaviour
    {
        public Action<InteractableBase> OnInteractableChanged;

        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Vector2 _interactionSize = Vector2.zero;
        [SerializeField] private float _interactionDistance = 0.5f;

        private PawnController _pawn;
        private InteractableBase _interactable;
        private RaycastHit2D _hit;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void OnFixedUpdate()
        {
            _hit = Physics2D.BoxCast(_interactionPoint.position, _interactionSize, 0f, transform.right, _interactionDistance, WorldManager.StaticInstance.LayerManager.InteractableMask);
            if (_hit.collider != null)
            {
                InteractableBase interactable = _hit.collider.GetComponentInParent<InteractableBase>();
                if (interactable != null)
                {
                    if (interactable != _interactable)
                    {
                        _interactable = interactable;
                        OnInteractableChanged?.Invoke(_interactable);
                    }
                }
                else if (_hit.collider.TryGetComponent(out interactable))
                {
                    if (interactable != _interactable)
                    {
                        _interactable = interactable;
                        OnInteractableChanged?.Invoke(_interactable);
                    }
                }
                else if (_interactable != null)
                {
                    _interactable = null;
                    OnInteractableChanged?.Invoke(null);
                }
            }
            else if (_interactable != null)
            {
                _interactable = null;
                OnInteractableChanged?.Invoke(null);
            }
        }

        public void Interact()
        {
            if (_interactable == null || _pawn.IsDead || _pawn.IsPerfomingAction)
            {
                return;
            }
            _interactable.Interact(_pawn);
            _interactable = null;
            OnInteractableChanged?.Invoke(null);
        }

        private void OnDrawGizmos()
        {
            if (_interactionPoint != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(_interactionPoint.position, _interactionSize);
                Gizmos.DrawWireCube(_interactionPoint.position + transform.right * _interactionDistance, _interactionSize);
            }
        }
    }
}