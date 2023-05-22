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

        public static UnityAction<int> OnEnemyDeath; 

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                GetComponent<Animator>().SetTrigger("Death");
                OnEnemyDeath(_pointsValue);
                Destroy(gameObject, _despawnDelay);
            }
        }
    }
}