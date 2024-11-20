using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private Rigidbody2D _rb;

        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _timeToJump = 0.25f;
        [SerializeField] private float _timeToFall = 0.25f;
        [SerializeField] private Transform _groundOverlapPoint;
        [SerializeField] private Vector2 _groundOverlapSize;
        [SerializeField] private LayerMask _obstacleMask;

        private Vector2 _moveDirection;
        private float _jumpTime;
        private float _groundedTime;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _rb = GetComponentInParent<Rigidbody2D>();
        }

        public void HandleLocomotion(Vector2 direction)
        {
            _moveDirection = direction;
            if (_pawn.CanJump && _jumpTime > 0f && _groundedTime > 0f)
            {
                _jumpTime = 0f;
                _groundedTime = 0f;
                ApplyJumpForce();
            }
            _pawn.IsGrounded = _rb.linearVelocity.y <= 0f && Physics2D.OverlapBox(_groundOverlapPoint.position, _groundOverlapSize, 0f, _obstacleMask);
            if (_pawn.IsGrounded)
            {
                _groundedTime = _timeToFall;
            }
            else
            {
                _groundedTime -= Time.fixedDeltaTime;
            }
            _jumpTime -= Time.fixedDeltaTime;
            if (_pawn.CanMove && _moveDirection.x != 0f && Mathf.Abs(_rb.linearVelocity.x) < _maxSpeed)
            {
                _rb.AddForce(_acceleration * _moveDirection.x * Vector2.right);
            }
            _pawn.PawnAnimator.SetBool("IsMoving", _rb.linearVelocityX != 0f);
            _pawn.PawnAnimator.SetBool("IsGrounded", _pawn.IsGrounded);
            _pawn.PawnAnimator.SetFloat("HorizontalVelocity", _rb.linearVelocityX * (_pawn.PawnAnimator.IsFacingRight ? 1f : -1f) / _maxSpeed);
            _pawn.PawnAnimator.SetFloat("VerticalVelocity", _rb.linearVelocityY);
            _pawn.PawnAnimator.SetFloat("MoveSpeed", Mathf.Abs(_rb.linearVelocityX));
        }

        private void ApplyJumpForce()
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Jump()
        {
            _jumpTime = _timeToJump;
        }

        private void OnDrawGizmos()
        {
            if (_groundOverlapPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(_groundOverlapPoint.position, _groundOverlapSize);
            }
        }
    }
}