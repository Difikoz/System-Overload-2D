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
        public float AttackDamage = 10f;
        public float AttackPoise = 25f;
        public float Health = 100f;
        public float HealthMax = 100f;
        public float HealthRegeneration = 1f;
        public float Poise = 0f;
        public float PoiseMax = 50f;

        [Header("Regeneration")]
        [SerializeField] private float _healthRegenerationTickCooldown = 0.5f;
        [SerializeField] private float _healthRegenerationDelayCooldown = 5f;
        [SerializeField] private float _poiseResetCooldown = 4f;

        private PawnController _pawn;
        [Header("Visible For Debug")]
        [SerializeField] private float _healthRegenerationTickTime;
        [SerializeField] private float _healthRegenerationDelayTime;
        [SerializeField] private float _poiseResetTime;

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
            if (Poise > 0f)
            {
                _poiseResetTime += Time.fixedDeltaTime;
                if (_poiseResetTime >= _poiseResetCooldown)
                {
                    ResetPoise();
                }
            }
        }

        public void TakeDamage(float damage, float poise, PawnController source = null)
        {
            if (_pawn.IsDead)
            {
                return;
            }
            _healthRegenerationDelayTime = 0f;
            Health = Mathf.Clamp(Health - damage, 0f, HealthMax);
            Poise = Mathf.Clamp(Poise + poise, 0f, PoiseMax);
            if (Health <= 0f)
            {
                _pawn.Die(source);
            }
            else
            {
                if (Poise >= PoiseMax)
                {
                    BreakPoise();
                }
                OnHealthChanged?.Invoke(Health, HealthMax);
            }
            _poiseResetTime = 0f;
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

        public void BreakPoise()
        {
            Poise = 0f;
            _pawn.PawnAnimator.PlayAction("Get Hit");
        }

        public void ResetPoise()
        {
            Poise = 0f;
            _poiseResetTime = 0f;
        }
    }
}