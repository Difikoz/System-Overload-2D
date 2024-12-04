using UnityEngine;

namespace WinterUniverse
{
    public class PawnUI : MonoBehaviour
    {
        [SerializeField] private VitalityBar _healthBar;

        private PawnController _pawn;
        private Canvas _canvas;

        public Canvas Canvas => _canvas;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _canvas = GetComponentInChildren<Canvas>();
            _canvas.worldCamera = Camera.main;
            _pawn.PawnStats.OnHealthChanged += _healthBar.SetValues;
            _healthBar.SetValues(_pawn.PawnStats.Health, _pawn.PawnStats.HealthMax);
        }

        public void OnDespawn()
        {
            _pawn.PawnStats.OnHealthChanged -= _healthBar.SetValues;
        }
    }
}