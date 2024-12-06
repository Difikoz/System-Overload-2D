using System;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        public Action OnDied;

        public string PawnName = "Faceless";
        public Vector2 MoveDirection;
        public bool IsGrounded;
        public bool IsKnockbacked;
        public bool IsMoving;
        public bool IsDashing;
        public bool IsAttacking;
        public bool IsFacingRight;
        public bool IsPerfomingAction;
        public bool CanMove;
        public bool CanJump;
        public bool CanDash;
        public bool IsDead;

        protected PawnAnimator _pawnAnimator;
        protected PawnCombat _pawnCombat;
        protected PawnInteraction _pawnInteraction;
        protected PawnInventory _pawnInventory;
        protected PawnLocomotion _pawnLocomotion;
        protected PawnSound _pawnSound;
        protected PawnStats _pawnStats;
        protected PawnUI _pawnUI;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnInteraction PawnInteraction => _pawnInteraction;
        public PawnInventory PawnInventory => _pawnInventory;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnSound PawnSound => _pawnSound;
        public PawnStats PawnStats => _pawnStats;
        public PawnUI PawnUI => _pawnUI;

        public virtual void Initialize()
        {
            GetComponents();
            InitializeComponents();
        }

        protected virtual void GetComponents()
        {
            _pawnAnimator = GetComponent<PawnAnimator>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnInteraction = GetComponent<PawnInteraction>();
            _pawnInventory = GetComponent<PawnInventory>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnSound = GetComponent<PawnSound>();
            _pawnStats = GetComponent<PawnStats>();
            _pawnUI = GetComponentInChildren<PawnUI>();
        }

        protected virtual void InitializeComponents()
        {
            IsGrounded = true;
            IsKnockbacked = false;
            IsMoving = false;
            IsDashing = false;
            IsAttacking = false;
            IsPerfomingAction = false;
            CanMove = true;
            CanJump = true;
            CanDash = true;
            IsDead = false;
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnInteraction.Initialize();
            _pawnInventory.Initialize();
            _pawnLocomotion.Initialize();
            _pawnSound.Initialize();
            _pawnStats.Initialize();
            _pawnUI.Initialize();
        }

        public virtual void Despawn()
        {
            _pawnUI.OnDespawn();
        }

        public virtual void OnFixedUpdate()
        {
            _pawnLocomotion.OnFixedUpdate();
            _pawnAnimator.OnFixedUpdate();
            _pawnCombat.OnFixedUpdate();
            _pawnStats.OnFixedUpdate();
            _pawnInteraction.OnFixedUpdate();
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
            _pawnSound.PlayDeathClip();
            _pawnAnimator.PlayAction("Death");
            OnDied?.Invoke();
            PerformDeath();
        }

        protected virtual void PerformDeath()
        {

        }
    }
}