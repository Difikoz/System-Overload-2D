using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected string _interactableTextBase = "Any Action";
        [SerializeField] protected Transform _interactionPoint;
        [SerializeField] protected bool _isInteractiveForAI = false;

        public Transform InteractionPoint => _interactionPoint != null ? _interactionPoint : transform;
        public bool IsInteractiveForAI => _isInteractiveForAI;

        public virtual string GetText()
        {
            return $"{_interactableTextBase}";
        }

        public abstract void Interact(PawnController pawn);
    }
}