using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionShifters.Player
{
    public class ARInput : MonoBehaviour
    {
        [SerializeField]
        private Image _targetingImage = null;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                ShootRay();
            }
        }

        private void ShootRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(_targetingImage.transform.position);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.TryGetComponent<Enemies.EnemyHealth>(out var enemyHealth))
                {
                    enemyHealth.TakeDamage(1);
                }
            }
        }
    }
}