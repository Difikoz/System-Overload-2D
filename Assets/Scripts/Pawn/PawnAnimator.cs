using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        protected PawnController _pawn;
        protected Animator _animator;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true, bool canMove = false, bool canJump = false)
        {
            _pawn.IsPerfomingAction = isPerfomingAction;
            _pawn.CanMove = canMove;
            _pawn.CanJump = canJump;
            _animator.CrossFade(name, fadeTime);
        }
    }
}