using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        private Vector2 _cursorInput;

        public void OnMove(InputValue value)
        {
            _moveDirection = value.Get<Vector2>();
        }

        public void OnCursor(InputValue value)
        {
            _cursorInput = value.Get<Vector2>();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void FixedUpdate()
        {
            _lookPoint = Camera.main.ScreenToWorldPoint(_cursorInput);
            base.FixedUpdate();
        }
    }
}