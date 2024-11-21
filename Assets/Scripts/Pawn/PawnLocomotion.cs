using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PawnLocomotion : MonoBehaviour
    {
        [SerializeField] private float _timeToJump = 0.25f;
        [SerializeField] private float _timeToFall = 0.25f;
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private Vector2 _groundCheckSize;
        [SerializeField] private LayerMask _groundMask;

        private PawnController _pawn;
        private Rigidbody2D _rb;
        private float _jumpTime;
        private float _groundedTime;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnFixedUpdate()
        {
            if (_pawn.CanJump && _jumpTime > 0f && _groundedTime > 0f)
            {
                _jumpTime = 0f;
                _groundedTime = 0f;
                _rb.AddForce(Vector2.up * _pawn.JumpForce, ForceMode2D.Impulse);
            }
            _pawn.IsGrounded = _rb.linearVelocityY <= 0f && Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0f, _groundMask);
            if (_pawn.IsGrounded)
            {
                _groundedTime = _timeToFall;
            }
            else
            {
                _groundedTime -= Time.fixedDeltaTime;
            }
            _jumpTime -= Time.fixedDeltaTime;
            if (_pawn.CanMove && _pawn.MoveDirection.x != 0f && Mathf.Abs(_rb.linearVelocityX) < _pawn.MaxSpeed)
            {
                _rb.AddForce(Vector2.right * _pawn.MoveDirection.x * _pawn.Acceleration);
                if (_pawn.IsFacingRight && _pawn.MoveDirection.x < 0f)
                {
                    _pawn.IsFacingRight = false;
                    transform.localScale = new(-1f, 1f, 1f);
                }
                else if (!_pawn.IsFacingRight && _pawn.MoveDirection.x > 0f)
                {
                    _pawn.IsFacingRight = true;
                    transform.localScale = new(1f, 1f, 1f);
                }
            }
            _pawn.PawnAnimator.SetFloat("Velocity", Mathf.Abs(_rb.linearVelocityX));
            _pawn.PawnAnimator.SetBool("IsGrounded", _pawn.IsGrounded);
        }

        public void PerformJump()
        {
            if (!_pawn.CanJump)
            {
                return;
            }
            _jumpTime = _timeToJump;
        }
    }
}