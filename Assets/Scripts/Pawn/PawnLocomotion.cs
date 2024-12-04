using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PawnLocomotion : MonoBehaviour
    {
        [SerializeField] private float _timeToJump = 0.25f;
        [SerializeField] private float _timeToFall = 0.25f;
        [SerializeField] private float _timeToDash = 0.25f;
        [SerializeField] private float _dashCooldown = 2f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _maxFallSpeed = 25f;
        [SerializeField] private float _jumpGravityMultiplier = 1f;
        [SerializeField] private float _fallGravityMultiplier = 2f;
        [SerializeField] private Transform _groundCheckPoint;
        [SerializeField] private Vector2 _groundCheckSize;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _wallCheckPoint;
        [SerializeField] private Vector2 _wallCheckSize;
        [SerializeField] private LayerMask _wallMask;
        [SerializeField] private Transform _roofCheckPoint;
        [SerializeField] private Vector2 _roofCheckSize;
        [SerializeField] private LayerMask _roofMask;

        private PawnController _pawn;
        private Rigidbody2D _rb;
        [Header("Visible For Debug")]
        [SerializeField] private float _movementVelocity;
        [SerializeField] private float _dashVelocity;
        [SerializeField] private float _fallVelocity;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _groundedTime;
        [SerializeField] private float _dashTime;
        [SerializeField] private int _jumpCount;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnFixedUpdate()
        {
            HandleGravity();
            HandleMovement();
            HandleVelocity();
        }

        private void HandleGravity()
        {
            if (_pawn.CanJump && _jumpTime > 0f && _groundedTime > 0f)
            {
                ApplyJumpForce();
            }
            _pawn.IsGrounded = _fallVelocity <= 0f && Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0f, _groundMask);
            if (_pawn.IsGrounded)
            {
                _groundedTime = _timeToFall;
                _fallVelocity = 0f;
                _jumpCount = 0;
            }
            else if (!_pawn.IsDashing)
            {
                if (UnderRoof())
                {
                    _fallVelocity = 0f;
                }
                _groundedTime -= Time.fixedDeltaTime;
                if (_fallVelocity >= 0f)
                {
                    _fallVelocity += _gravity * _jumpGravityMultiplier * Time.fixedDeltaTime;
                }
                else if (_fallVelocity > -_maxFallSpeed)
                {
                    _fallVelocity += _gravity * _fallGravityMultiplier * Time.fixedDeltaTime;
                }
            }
            _jumpTime -= Time.fixedDeltaTime;
        }

        private void HandleMovement()
        {
            if (_dashVelocity != 0f)
            {
                _dashVelocity = Mathf.MoveTowards(_dashVelocity, 0f, _pawn.PawnStats.DashForce / _timeToDash * Time.fixedDeltaTime);
            }
            if (_pawn.CanMove && _pawn.MoveDirection.x != 0f)
            {
                _movementVelocity = Mathf.MoveTowards(_movementVelocity, _pawn.MoveDirection.x * _pawn.PawnStats.MaxSpeed, _pawn.PawnStats.Acceleration * Time.fixedDeltaTime);
                if (_pawn.IsFacingRight && _pawn.MoveDirection.x < 0f)
                {
                    _pawn.FlipLeft();
                }
                else if (!_pawn.IsFacingRight && _pawn.MoveDirection.x > 0f)
                {
                    _pawn.FlipRight();
                }
            }
            else
            {
                _movementVelocity = Mathf.MoveTowards(_movementVelocity, 0f, _pawn.PawnStats.Deceleration * Time.fixedDeltaTime);
            }
            if (FacedToWall())
            {
                _movementVelocity = 0f;
                _dashVelocity = 0f;
            }
            _pawn.IsMoving = _movementVelocity != 0f;
            _pawn.IsDashing = _dashVelocity != 0f;
        }

        private void HandleVelocity()
        {
            _rb.linearVelocityX = _movementVelocity + _dashVelocity;
            _rb.linearVelocityY = _fallVelocity;
        }

        private bool UnderRoof()
        {
            return _fallVelocity > 0f && Physics2D.OverlapBox(_roofCheckPoint.position, _roofCheckSize, 0f, _roofMask);
        }

        private bool FacedToWall()
        {
            if (_pawn.IsFacingRight && (_movementVelocity > 0f || _dashVelocity > 0f))
            {
                return Physics2D.OverlapBox(_wallCheckPoint.position, _wallCheckSize, 0f, _wallMask);
            }
            else if (!_pawn.IsFacingRight && (_movementVelocity < 0f || _dashVelocity < 0f))
            {
                return Physics2D.OverlapBox(_wallCheckPoint.position, _wallCheckSize, 0f, _wallMask);
            }
            return false;
        }

        public void StartJumping()
        {
            if (!_pawn.CanJump || _jumpCount >= _pawn.PawnStats.JumpCount)
            {
                return;
            }
            if (_jumpCount == 0)
            {
                _jumpTime = _timeToJump;
            }
            else
            {
                ApplyJumpForce();
            }
        }

        public void StopJumping()
        {
            if (_fallVelocity > 0f)
            {
                _fallVelocity /= 2f;
            }
        }

        public void PerformDash()
        {
            if (!_pawn.CanDash || Time.time < _dashTime + _dashCooldown)
            {
                return;
            }
            _dashTime = Time.time;
            _movementVelocity = 0f;
            _fallVelocity = 0f;
            _dashVelocity = transform.localScale.x * _pawn.PawnStats.DashForce;
            _pawn.IsDashing = true;
            _pawn.PawnAnimator.PlayAction("Dash");
        }

        private void ApplyJumpForce()
        {
            _jumpCount++;
            _jumpTime = 0f;
            _groundedTime = 0f;
            _fallVelocity = _pawn.PawnStats.JumpForce;
        }

        private void OnDrawGizmos()
        {
            if (_groundCheckPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
            }
            if (_wallCheckPoint != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(_wallCheckPoint.position, _wallCheckSize);
            }
            if (_roofCheckPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(_roofCheckPoint.position, _roofCheckSize);
            }
        }
    }
}