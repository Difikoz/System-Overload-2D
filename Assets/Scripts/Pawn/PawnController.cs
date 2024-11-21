using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : MonoBehaviour
    {
        public Vector2 MoveDirection;
        public float Acceleration = 8f;
        public float MaxSpeed = 8f;
        public float JumpForce = 8f;
        public bool IsGrounded = true;
        public bool IsFacingRight = true;
        public bool IsPerfomingAction;
        public bool CanMove = true;
        public bool CanJump = true;

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
        }
    }
}