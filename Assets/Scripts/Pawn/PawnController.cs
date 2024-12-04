using System;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        public Action OnDied;

        public Vector2 MoveDirection;
        public bool IsGrounded = true;
        public bool IsMoving;
        public bool IsDashing;
        public bool IsAttacking;
        public bool IsFacingRight = true;
        public bool IsPerfomingAction;
        public bool CanMove = true;
        public bool CanJump = true;
        public bool CanDash = true;
        public bool IsDead;

        protected PawnAnimator _pawnAnimator;
        protected PawnCombat _pawnCombat;
        protected PawnLocomotion _pawnLocomotion;
        protected PawnStats _pawnStats;
        protected PawnUI _pawnUI;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnStats PawnStats => _pawnStats;
        public PawnUI PawnUI => _pawnUI;

        protected virtual void Awake()
        {
            GetComponents();
            InitializeComponents();
        }

        protected virtual void GetComponents()
        {
            _pawnAnimator = GetComponent<PawnAnimator>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnStats = GetComponent<PawnStats>();
            _pawnUI = GetComponent<PawnUI>();
        }

        protected virtual void InitializeComponents()
        {
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnLocomotion.Initialize();
            _pawnStats.Initialize();
            _pawnUI.Initialize();
        }

        protected virtual void OnDespawn()
        {
            _pawnUI.OnDespawn();
        }

        protected virtual void FixedUpdate()
        {
            _pawnLocomotion.OnFixedUpdate();
            _pawnAnimator.OnFixedUpdate();
            _pawnCombat.OnFixedUpdate();
            _pawnStats.OnFixedUpdate();
        }

        public void FlipRight()
        {
            IsFacingRight = true;
            transform.localScale = new(1f, 1f, 1f);
            _pawnUI.Canvas.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        public void FlipLeft()
        {
            IsFacingRight = false;
            transform.localScale = new(-1f, 1f, 1f);
            _pawnUI.Canvas.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }

        public void Die(bool spawnBlood = true, PawnController source = null)
        {
            if (IsDead)
            {
                return;
            }
            if (spawnBlood)
            {
                _pawnCombat.SpawnBlood();
            }
            _pawnStats.Health = 0f;
            _pawnStats.OnHealthChanged?.Invoke(_pawnStats.Health, _pawnStats.HealthMax);
            IsDead = true;
            _pawnAnimator.PlayAction("Death");
            OnDied?.Invoke();
            PerformDeath();
        }

        protected virtual void PerformDeath()
        {
            Destroy(gameObject, 5f);
        }
    }
}