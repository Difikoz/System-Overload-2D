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

        public void OnDash()
        {
            _pawnLocomotion.PerformDash();
        }

        public void OnAttack(InputValue value)
        {
            IsAttacking = value.isPressed;
        }

        public void OnInteract()
        {
            _pawnInteraction.Interact();
        }

        public void OnStatus()
        {
            WorldManager.StaticInstance.UIManager.ToggleStatusMenu();
        }
    }
}