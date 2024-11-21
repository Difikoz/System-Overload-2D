using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        public void OnMove(InputValue value)
        {
            MoveDirection = value.Get<Vector2>();
        }

        public void OnJump()
        {
            _pawnLocomotion.PerformJump();
        }

        public void OnAttack()
        {

        }

        protected override void Awake()
        {
            base.Awake();
            Invoke(nameof(LateStart), 0.25f);
        }

        private void LateStart()
        {
            FindFirstObjectByType<CameraController>().SetTarget(transform, new(0f, 1f, 0f), 10f);
        }
    }
}