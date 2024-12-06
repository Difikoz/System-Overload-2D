using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractableBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject _barWindow;
        [SerializeField] private TMP_Text _interactableText;

        public void UpdateText(InteractableBase interactable)
        {
            if (interactable != null)
            {
                _interactableText.text = interactable.GetText();
                _barWindow.SetActive(true);
            }
            else
            {
                _barWindow.SetActive(false);
            }
        }
    }
}