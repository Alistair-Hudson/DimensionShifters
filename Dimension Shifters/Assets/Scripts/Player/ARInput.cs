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
        [SerializeField]
        private ParticleSystem _laserShot = null;
        [SerializeField]
        private AudioClip _laserSound = null;

        private AudioSource _audioSource = null;

        private bool _canShoot = true;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

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
            _laserShot.Emit(1);
            _audioSource.PlayOneShot(_laserSound);
        }
    }
}