using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStats : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;

        public float Acceleration = 8f;
        public float Deceleration = 16f;
        public float MaxSpeed = 4f;
        public float JumpForce = 8f;
        public int JumpCount = 1;
        public float AttackDamage = 25f;
        public float Health = 100f;
        public float HealthMax = 100f;
        public float HealthRegeneration = 1f;

        [Header("Regeneration")]
        [SerializeField] private float _healthRegenerationTickCooldown = 0.5f;
        [SerializeField] private float _healthRegenerationDelayCooldown = 5f;

        private PawnController _pawn;
        [Header("Visible For Debug")]
        [SerializeField] private float _healthRegenerationTickTime;
        [SerializeField] private float _healthRegenerationDelayTime;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            OnHealthChanged?.Invoke(Health, HealthMax);
        }

        public void OnFixedUpdate()
        {
            if (_healthRegenerationDelayTime >= _healthRegenerationDelayCooldown)
            {
                if (_healthRegenerationTickTime >= _healthRegenerationTickCooldown)
                {
                    RestoreHealth(HealthRegeneration);
                    _healthRegenerationTickTime = 0f;
                }
                else
                {
                    _healthRegenerationTickTime += Time.fixedDeltaTime;
                }
            }
            else
            {
                _healthRegenerationDelayTime += Time.fixedDeltaTime;
            }
        }

        public void TakeDamage(float value, PawnController source = null)
        {
            if (_pawn.IsDead)
            {
                return;
            }
            _healthRegenerationDelayTime = 0f;
            Health = Mathf.Clamp(Health - value, 0f, HealthMax);
            if (Health <= 0f)
            {
                _pawn.Die(source);
            }
            else
            {
                OnHealthChanged?.Invoke(Health, HealthMax);
            }
        }

        public void RestoreHealth(float value)
        {
            if (_pawn.IsDead || Health == HealthMax)
            {
                return;
            }
            Health = Mathf.Clamp(Health + value, 0f, HealthMax);
            OnHealthChanged?.Invoke(Health, HealthMax);
        }
    }
}