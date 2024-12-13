using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        public void OnMove(InputValue value)
        {
            MoveDirection = InputEnabled ? value.Get<Vector2>() : Vector2.zero;
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed && InputEnabled)
            {
                _pawnLocomotion.StartJumping();
            }
            else
            {
                _pawnLocomotion.StopJumping();
            }
        }

        public void OnDash()
        {
            if (!InputEnabled)
            {
                return;
            }
            _pawnLocomotion.PerformDash();
        }

        public void OnAttack(InputValue value)
        {
            IsAttacking = value.isPressed && InputEnabled;
        }

        public void OnInteract()
        {
            if (!InputEnabled)
            {
                return;
            }
            _pawnInteraction.Interact();
        }

        public void OnStatus()
        {
            WorldManager.StaticInstance.UIManager.ToggleStatusMenu();
        }
    }
}