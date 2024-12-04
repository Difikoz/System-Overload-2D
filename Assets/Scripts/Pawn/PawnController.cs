using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        public Vector2 MoveDirection;
        public float Acceleration = 8f;
        public float Deceleration = 16f;
        public float MaxSpeed = 4f;
        public float JumpForce = 8f;
        public int JumpCount = 1;
        public bool IsGrounded = true;
        public bool IsMoving;
        public bool IsAttacking;
        public bool IsFacingRight = true;
        public bool IsPerfomingAction;
        public bool CanMove = true;
        public bool CanJump = true;
        public bool IsDead;

        protected PawnLocomotion _pawnLocomotion;
        protected PawnAnimator _pawnAnimator;
        protected PawnCombat _pawnCombat;

        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;

        protected virtual void Awake()
        {
            GetComponents();
            InitializeComponents();
        }

        protected virtual void GetComponents()
        {
            _pawnLocomotion = GetComponentInChildren<PawnLocomotion>();
            _pawnAnimator = GetComponentInChildren<PawnAnimator>();
            _pawnCombat = GetComponentInChildren<PawnCombat>();
        }

        protected virtual void InitializeComponents()
        {
            _pawnLocomotion.Initialize();
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
        }

        protected virtual void FixedUpdate()
        {
            _pawnLocomotion.OnFixedUpdate();
            _pawnAnimator.OnFixedUpdate();
            _pawnCombat.OnFixedUpdate();
        }

        public void Die()
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