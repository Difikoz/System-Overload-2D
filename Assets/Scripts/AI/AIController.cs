using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class AIController : PawnController
    {
        public override void Despawn()
        {
            base.Despawn();
            LeanPool.Despawn(gameObject);
        }
    }
}