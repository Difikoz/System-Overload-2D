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
        [SerializeField] private float _lookSpeed = 180f;

        public bool IsFacingRight => _isFacingRight;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void HandleRotation(Vector3 position)
        {
            _lookDirection = position - _lookBone.position;
            _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
            _lookBone.rotation = Quaternion.Slerp(_lookBone.rotation, Quaternion.Euler(0f, 0f, _lookAngle), _lookSpeed * Time.fixedDeltaTime);
            if (_lookDirection.x > 0f)
            {
                _animator.transform.rotation = Quaternion.Slerp(_animator.transform.rotation, Quaternion.Euler(0f, 0f, 0f), _lookSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _animator.transform.rotation = Quaternion.Slerp(_animator.transform.rotation, Quaternion.Euler(0f, 180f, 0f), _lookSpeed * Time.fixedDeltaTime);
            }
            //if (_isFacingRight && _lookDirection.x < 0f)
            //{
            //    FlipLeft();
            //}
            //else if (!_isFacingRight && _lookDirection.x > 0f)
            //{
            //    FlipRight();
            //}
        }

        private void FlipRight()
        {
            _isFacingRight = true;
            _animator.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void FlipLeft()
        {
            _isFacingRight = false;
            _animator.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void SetTrigger(string name)// in future change to play action
        {
            _animator.SetTrigger(name);
        }
    }
}