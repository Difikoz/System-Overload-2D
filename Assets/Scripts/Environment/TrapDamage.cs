using UnityEngine;

namespace WinterUniverse
{
    public class TrapDamage : TrapBase
    {
        [SerializeField] private float _damageOnEnter = 10f;
        [SerializeField] private float _damageOnStay = 10f;
        [SerializeField] private float _damageOnExit = 10f;

        protected override void OnEnterAction(PawnController pawn)
        {
            base.OnEnterAction(pawn);
            pawn.PawnStats.TakeDamage(_damageOnEnter);
        }

        protected override void OnStayAction(PawnController pawn)
        {
            base.OnStayAction(pawn);
            pawn.PawnStats.TakeDamage(_damageOnStay);
        }

        protected override void OnExitAction(PawnController pawn)
        {
            base.OnExitAction(pawn);
            pawn.PawnStats.TakeDamage(_damageOnExit);
        }
    }
}