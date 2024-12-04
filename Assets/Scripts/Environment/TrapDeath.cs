using UnityEngine;

namespace WinterUniverse
{
    public class TrapDeath : TrapBase
    {
        protected override void OnEnterAction(PawnController pawn)
        {
            base.OnEnterAction(pawn);
            pawn.Die();
        }
    }
}