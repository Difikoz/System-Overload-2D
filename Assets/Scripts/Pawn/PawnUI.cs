using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;

        private PawnController _pawn;
        private Canvas _canvas;

        public Canvas Canvas => _canvas;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _canvas = GetComponentInChildren<Canvas>();
            _canvas.worldCamera = Camera.main;
            _nameText.text = _pawn.PawnName;
            _pawn.PawnStats.OnHealthChanged += _healthBar.SetValues;
            _pawn.PawnStats.OnEnergyChanged += _energyBar.SetValues;
            _healthBar.SetValues(_pawn.PawnStats.Health, _pawn.PawnStats.HealthMax);
            _energyBar.SetValues(_pawn.PawnStats.Energy, _pawn.PawnStats.EnergyMax);
        }

        public void OnDespawn()
        {
            _pawn.PawnStats.OnHealthChanged -= _healthBar.SetValues;
            _pawn.PawnStats.OnEnergyChanged -= _energyBar.SetValues;
        }
    }
}