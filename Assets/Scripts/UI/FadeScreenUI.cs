using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class FadeScreenUI : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;

        private Color _color;

        public void SetFadeAmount(float percent)
        {
            _color.a = percent;
            _fadeImage.color = _color;
        }
    }
}