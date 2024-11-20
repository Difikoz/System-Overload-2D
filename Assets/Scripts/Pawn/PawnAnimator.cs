using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;
        private Vector2 _lookDirection;
        private float _lookAngle;
        private bool _isFacingRight = true;

        [SerializeField] private Transform _lookBone;

        public bool IsFacingRight => _isFacingRight;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void HandleRotation(Vector3 position)
        {
            if (!_pawn.CanRotate)
            {
                return;
            }
            _lookDirection = position - _lookBone.position;
            _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
            if (_lookDirection.x < 0f)
            {
                LookLeft();
            }
            else if (_lookDirection.x > 0f)
            {
                LookRight();
            }
        }

        private void LookRight()
        {
            _isFacingRight = true;
            _animator.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _lookBone.rotation = Quaternion.Euler(0f, 0f, _lookAngle);
        }

        private void LookLeft()
        {
            _isFacingRight = false;
            _animator.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _lookBone.rotation = Quaternion.Euler(180f, 0f, -_lookAngle);
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool canMove = false, bool canRotate = false, bool canJump = false)
        {
            _pawn.CanMove = canMove;
            _pawn.CanRotate = canRotate;
            _pawn.CanJump = canJump;
            _animator.CrossFade(name, fadeTime);
        }
    }
}