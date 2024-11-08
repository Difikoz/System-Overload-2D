using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        private PawnController _pawn;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void HandleTargeting(bool isAttacking)
        {
            if (isAttacking)
            {
                _pawn.PawnAnimator.SetBool("IsAttacking", true);
                // fire
            }
            else
            {
                _pawn.PawnAnimator.SetBool("IsAttacking", false);
            }
        }
    }
}