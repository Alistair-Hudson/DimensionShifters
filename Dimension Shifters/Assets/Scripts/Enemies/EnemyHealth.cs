using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private int _health = 3;
        [SerializeField]
        private float _despawnDelay = 1f;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                GetComponent<Animator>().SetTrigger("Death");
                Destroy(gameObject, _despawnDelay);
            }
        }
    }
}