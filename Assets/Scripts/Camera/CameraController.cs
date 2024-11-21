using UnityEngine;

namespace WinterUniverse
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _offset;
        private float _followSpeed;

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _followSpeed * Time.deltaTime);
        }

        public void SetTarget(Transform target, Vector3 offset, float followSpeed)
        {
            if (target != null)
            {
                _target = target;
            }
            else
            {
                _target = null;
            }
            _offset = offset;
            _followSpeed = followSpeed;
        }
    }
}