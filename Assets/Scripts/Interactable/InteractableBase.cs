using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] private Transform _interactionPoint;

        public Transform InteractionPoint => _interactionPoint != null ? _interactionPoint : transform;

        public abstract string GetText();
        public abstract void Interact(PawnController pawn);
    }
}