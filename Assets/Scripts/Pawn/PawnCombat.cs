using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        [SerializeField] private GameObject _attackEffect;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Vector2 _attackSize;
        [SerializeField] private LayerMask _damageableMask;
        [SerializeField] private Transform _bloodSpawnPoint;
        [SerializeField] private GameObject _bloodEffect;

        protected PawnController _pawn;
        protected PawnController _target;
        private List<PawnController> _damagedTargets = new();

        public PawnController Target => _target;

        public virtual void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _attackEffect.SetActive(false);
        }

        public void OnFixedUpdate()
        {
            if (_pawn.IsAttacking && !_pawn.IsPerfomingAction)
            {
                _pawn.PawnAnimator.PlayAction("Attack");
            }
        }

        public void SpawnBlood()
        {
            GameObject effect = Instantiate(_bloodEffect, _bloodSpawnPoint.position, Quaternion.identity);
            Destroy(effect, 10f);
        }

        public void PerformAttack()// called from animation event (!!!)
        {
            _attackEffect.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(_attackPoint.position, _attackSize, 0f, _damageableMask);
            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    PawnController pawn = collider.GetComponentInParent<PawnController>();
                    if (pawn != null && !_damagedTargets.Contains(pawn) && pawn != _pawn && !pawn.IsDead)
                    {
                        pawn.PawnStats.TakeDamage(_pawn.PawnStats.AttackDamage, _pawn.PawnStats.AttackPoise, true, _pawn);
                        _damagedTargets.Add(pawn);
                    }
                }
            }
        }

        public void CompleteAttack()// called from animation event (!!!)
        {
            _attackEffect.SetActive(false);
            _damagedTargets.Clear();
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

        private void OnDrawGizmos()
        {
            if (_attackPoint != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(_attackPoint.position, _attackSize);
            }
        }
    }
}