using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class TrapBase : MonoBehaviour
    {
        [SerializeField] private bool _triggerOnce;
        [SerializeField] private bool _activateOnEnter;
        [SerializeField] private bool _activateOnStay;
        [SerializeField] private bool _activateOnExit;
        [SerializeField] private bool _hasCooldown;
        [SerializeField] private float _cooldownTime = 2f;

        private bool _triggered;
        private Coroutine _coroutine;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_triggerOnce && _triggered)
            {
                return;
            }
            if (_hasCooldown && _coroutine != null)
            {
                return;
            }
            if (_activateOnEnter && collision.TryGetComponent(out PawnController pawn))
            {
                _coroutine = StartCoroutine(CooldownTimer());
                OnEnterAction(pawn);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_triggerOnce && _triggered)
            {
                return;
            }
            if (_hasCooldown && _coroutine != null)
            {
                return;
            }
            if (_activateOnStay && collision.TryGetComponent(out PawnController pawn))
            {
                _coroutine = StartCoroutine(CooldownTimer());
                OnStayAction(pawn);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_triggerOnce && _triggered)
            {
                return;
            }
            if (_hasCooldown && _coroutine != null)
            {
                return;
            }
            if (_activateOnExit && collision.TryGetComponent(out PawnController pawn))
            {
                _coroutine = StartCoroutine(CooldownTimer());
                OnExitAction(pawn);
            }
        }

        protected virtual void OnEnterAction(PawnController pawn)
        {

        }

        protected virtual void OnStayAction(PawnController pawn)
        {

        }

        protected virtual void OnExitAction(PawnController pawn)
        {

        }

        protected IEnumerator CooldownTimer()
        {
            yield return new WaitForSeconds(_cooldownTime);
            _coroutine = null;
        }
    }
}