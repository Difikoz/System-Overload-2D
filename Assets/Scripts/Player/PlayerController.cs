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

        public void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                _pawnLocomotion.StartJumping();
            }
            else
            {
                _pawnLocomotion.StopJumping();
            }
        }

        public void OnAttack(InputValue value)
        {
            IsAttacking = value.isPressed;
        }

        protected override void Awake()
        {
            base.Awake();
            Invoke(nameof(LateStart), 0.25f);
        }

        private void LateStart()
        {
            FindFirstObjectByType<CameraController>().SetTarget(transform, new(0f, 1f, 0f), 5f);
        }
    }
}