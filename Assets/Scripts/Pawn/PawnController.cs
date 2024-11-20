using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PawnController : MonoBehaviour
    {
        public bool CanMove = true;
        public bool CanRotate = true;
        public bool CanJump = true;
        public bool IsGrounded = true;

        private PawnLocomotion _pawnLocomotion;
        private PawnAnimator _pawnAnimator;
        private PawnCombat _pawnCombat;

        protected Vector2 _moveDirection;
        protected Vector2 _lookPoint;
        protected bool _isAttacking;

        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;

        protected virtual void Awake()
        {
            _pawnLocomotion = GetComponentInChildren<PawnLocomotion>();
            _pawnAnimator = GetComponentInChildren<PawnAnimator>();
            _pawnCombat = GetComponentInChildren<PawnCombat>();
            _pawnLocomotion.Initialize();
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
        }

        protected virtual void FixedUpdate()
        {
            _pawnLocomotion.HandleLocomotion(_moveDirection);
            _pawnAnimator.HandleRotation(_lookPoint);
        }
    }
}