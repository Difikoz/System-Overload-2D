using UnityEngine;

namespace WinterUniverse
{
    public class WorldUIManager : MonoBehaviour
    {
        private InteractableBarUI _interactableBar;

        public void Initialize()
        {
            _interactableBar = GetComponentInChildren<InteractableBarUI>();
            WorldManager.StaticInstance.Player.PawnInteraction.OnInteractableChanged += _interactableBar.UpdateText;
            _interactableBar.UpdateText(null);
        }

        public void OnDespawn()
        {
            WorldManager.StaticInstance.Player.PawnInteraction.OnInteractableChanged -= _interactableBar.UpdateText;
        }
    }
}