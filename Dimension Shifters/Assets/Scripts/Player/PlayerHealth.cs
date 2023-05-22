using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private int _baseHealth = 100;

        private int _health = 100;

        private void Awake()
        {
            _health = _baseHealth;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Debug.Log("GAME OVER");
            }
        }
    }
}