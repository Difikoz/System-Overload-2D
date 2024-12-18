using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStats : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnEnergyChanged;

        public float Acceleration = 8f;
        public float Deceleration = 16f;
        public float MaxSpeed = 4f;
        public float Mass = 80f;
        public float DashForce = 16f;
        public float JumpForce = 8f;
        public int JumpCount = 1;
        public float AttackDamage = 10f;
        public float AttackPoise = 25f;
        public float Health = 0f;
        public float HealthMax = 100f;
        public float HealthRegeneration = 1f;
        public float Energy = 0f;
        public float EnergyMax = 100f;
        public float EnergyRegeneration = 5f;
        public float Poise = 0f;
        public float PoiseMax = 50f;
        public float JumpEnergyCost = 10f;
        public float DashEnergyCost = 10f;
        public float AttackEnergyCost = 10f;

        [Header("Regeneration")]
        [SerializeField] private float _healthRegenerationTickCooldown = 1f;
        [SerializeField] private float _healthRegenerationDelayCooldown = 4f;
        [SerializeField] private float _energyRegenerationTickCooldown = 1f;
        [SerializeField] private float _energyRegenerationDelayCooldown = 2f;
        [SerializeField] private float _poiseResetCooldown = 4f;

        private PawnController _pawn;
        [Header("Visible For Debug")]
        [SerializeField] private float _healthRegenerationTickTime;
        [SerializeField] private float _healthRegenerationDelayTime;
        [SerializeField] private float _energyRegenerationTickTime;
        [SerializeField] private float _energyRegenerationDelayTime;
        [SerializeField] private float _poiseResetTime;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            ResetPoise();
            RestoreHealth(HealthMax);
            RestoreEnergy(EnergyMax);
            _healthRegenerationTickTime = 0f;
            _healthRegenerationDelayTime = 0f;
            _energyRegenerationTickTime = 0f;
            _energyRegenerationDelayTime = 0f;
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
            if (_energyRegenerationDelayTime >= _energyRegenerationDelayCooldown)
            {
                if (_energyRegenerationTickTime >= _energyRegenerationTickCooldown)
                {
                    RestoreEnergy(EnergyRegeneration);
                    _energyRegenerationTickTime = 0f;
                }
                else
                {
                    _energyRegenerationTickTime += Time.fixedDeltaTime;
                }
            }
            else
            {
                _energyRegenerationDelayTime += Time.fixedDeltaTime;
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

        public void TakeDamage(float damage, float poise, bool spawnBlood = true, PawnController source = null)
        {
            if (_pawn.IsDead)
            {
                return;
            }
            _healthRegenerationTickTime = 0f;
            _healthRegenerationDelayTime = 0f;
            Health = Mathf.Clamp(Health - damage, 0f, HealthMax);
            Poise = Mathf.Clamp(Poise + poise, 0f, PoiseMax);
            if (Health <= 0f)
            {
                _pawn.Die(spawnBlood, source);
            }
            else
            {
                if (spawnBlood)
                {
                    _pawn.PawnCombat.SpawnBlood();
                }
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

        public bool EnoughEnergy(float value)
        {
            return value >= 0f && Energy >= value;
        }

        public void ReduceEnergy(float value)
        {
            if (_pawn.IsDead)
            {
                return;
            }
            _energyRegenerationTickTime = 0f;
            _energyRegenerationDelayTime = 0f;
            Energy = Mathf.Clamp(Energy - value, 0f, EnergyMax);
            OnEnergyChanged?.Invoke(Energy, EnergyMax);
        }

        public void RestoreEnergy(float value)
        {
            if (_pawn.IsDead || Energy == EnergyMax)
            {
                return;
            }
            Energy = Mathf.Clamp(Energy + value, 0f, EnergyMax);
            OnEnergyChanged?.Invoke(Energy, EnergyMax);
        }

        public void BreakPoise()
        {
            Poise = 0f;
            _pawn.PawnSound.PlayGetHitClip();
            _pawn.PawnAnimator.PlayAction("Get Hit");
        }

        public void ResetPoise()
        {
            Poise = 0f;
            _poiseResetTime = 0f;
        }
    }
}