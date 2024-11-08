using UnityEngine;

namespace WinterUniverse
{
    public class DamageMultiplier : MonoBehaviour
    {
        [SerializeField] private float _value = 1f;

        public float Value => _value;
    }
}