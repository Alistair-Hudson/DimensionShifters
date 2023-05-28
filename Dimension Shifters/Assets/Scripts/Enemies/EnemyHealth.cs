using DimensionShifters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DimensionShifters.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private int _health = 3;
        [SerializeField]
        private int _pointsValue = 10;
        [SerializeField]
        private float _despawnDelay = 1f;

        public static UnityEvent<int> OnEnemyDeath = new UnityEvent<int>(); 

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                GetComponent<Animator>().SetTrigger("Death");
                OnEnemyDeath.Invoke(_pointsValue);
                GetComponent<Collider>().enabled = false;
                Destroy(gameObject, _despawnDelay);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!other.transform.parent.TryGetComponent<Camera>(out var player))
            {
                return;
            }
            Debug.Log($"{gameObject.name} has been hit by player");
            TakeDamage(1);
        }
    }
}