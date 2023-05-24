using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DimensionShifters.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private float _baseHealth = 100;

        private float _health = 100;

        public static UnityEvent<float> OnPlayerHealthChange = new UnityEvent<float>();

        private void Awake()
        {
            _health = _baseHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            OnPlayerHealthChange.Invoke(_health / _baseHealth);
            if (_health <= 0)
            {
                Debug.Log("GAME OVER");
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("hit by particle");
            TakeDamage(1);
        }
    }
}