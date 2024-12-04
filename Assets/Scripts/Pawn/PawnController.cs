using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        public Vector2 MoveDirection;
        public bool IsGrounded = true;
        public bool IsMoving;
        public bool IsAttacking;
        public bool IsFacingRight = true;
        public bool IsPerfomingAction;
        public bool CanMove = true;
        public bool CanJump = true;
        public bool IsDead;

        protected PawnAnimator _pawnAnimator;
        protected PawnCombat _pawnCombat;
        protected PawnLocomotion _pawnLocomotion;
        protected PawnStats _pawnStats;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnStats PawnStats => _pawnStats;

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
        }

        protected virtual void InitializeComponents()
        {
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnLocomotion.Initialize();
            _pawnStats.Initialize();
        }

        protected virtual void FixedUpdate()
        {
            _pawnLocomotion.OnFixedUpdate();
            _pawnAnimator.OnFixedUpdate();
            _pawnCombat.OnFixedUpdate();
            _pawnStats.OnFixedUpdate();
        }

        public void Die(PawnController source = null)
        {
            if (IsDead)
            {
                return;
            }
            IsDead = true;
            _pawnAnimator.PlayAction("Death");
            PerformDeath();
        }

        protected virtual void PerformDeath()
        {
            Destroy(gameObject, 5f);
        }
    }
}