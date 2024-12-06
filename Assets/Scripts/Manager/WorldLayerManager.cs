using UnityEngine;

namespace WinterUniverse
{
    public class WorldLayerManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private LayerMask _pawnMask;
        [SerializeField] private LayerMask _detectableMask;
        [SerializeField] private LayerMask _interactableMask;
        [SerializeField] private LayerMask _damageableMask;

        public LayerMask ObstacleMask => _obstacleMask;
        public LayerMask PawnMask => _pawnMask;
        public LayerMask DetectableMask => _detectableMask;
        public LayerMask InteractableMask => _interactableMask;
        public LayerMask DamageableMask => _damageableMask;
    }
}