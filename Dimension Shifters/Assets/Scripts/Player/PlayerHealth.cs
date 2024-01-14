using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DimensionShifters.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private float _baseHealth = 100;

        private float _health = 100;

        public static int LevelIndex { get; private set; }
        public static UnityEvent<float> OnPlayerHealthChange = new UnityEvent<float>();

        private void Awake()
        {
            _baseHealth = PlayerAtributes.PlayerHealth;
            _health = _baseHealth;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            OnPlayerHealthChange.Invoke(_health / _baseHealth);
            if (_health <= 0)
            {
                LevelIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene("GameOverScene");
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            TakeDamage(1);
        }
    }
}