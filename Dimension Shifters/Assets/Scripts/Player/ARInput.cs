using System;
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
        [SerializeField]
        private float _firingRate = 1;

        private bool _canShoot = true;

        private void Update()
        {
            if (!_canShoot)
            {
                return;
            }
            if (Input.touchCount > 0)
            {
                ShootRay();
                StartCoroutine(DelayNextShot());
            }
        }

        private IEnumerator DelayNextShot()
        {
            yield return new WaitForSeconds(1 / _firingRate);
            _canShoot = true;
        }

        private void ShootRay()
        {
            _canShoot = false;
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