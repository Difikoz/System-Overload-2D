using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        protected PawnController _pawn;
        protected PawnController _target;

        public PawnController Target => _target;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public virtual void SetTarget(PawnController target)
        {
            if (target != null)
            {
                _target = target;
            }
            else
            {
                _target = null;
            }
        }
    }
}