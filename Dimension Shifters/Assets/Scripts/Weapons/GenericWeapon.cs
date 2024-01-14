using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DimensionShifters.Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public class GenericWeapon : MonoBehaviour
    {
        [SerializeField]
        private AudioClip weaponSound = null;
        [SerializeField]
        private bool isScatterShot = false;

        private Projectile _projectilePrefab = null;
        private ProjectileSpawnPoint _projectileSpawnPoint = null;
        private AudioSource _audioSource = null;

        private int _weaponDamage = 1;

        private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

        public void SetUp(Projectile projectilePrefab, int weaponDamage)
        {
            _projectilePrefab = projectilePrefab;
            _projectileSpawnPoint = GetComponentInChildren<ProjectileSpawnPoint>();
            _audioSource = GetComponent<AudioSource>();

            _weaponDamage = weaponDamage;

            _particleSystems = GetComponentsInChildren<ParticleSystem>(true).ToList();
        }

        private void SpawProjectile(Vector3 offset)
        {
            var newProjectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.transform);
            newProjectile.transform.SetParent(null, true);
            newProjectile.transform.position += offset;
            newProjectile.Damage = _weaponDamage;
        }

        public void FireWeapon()
        {
            _audioSource.PlayOneShot(weaponSound);
            if (_projectilePrefab)
            {
                if (isScatterShot)
                {
                    SpawProjectile(new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                    SpawProjectile(new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                    SpawProjectile(new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
                }
                else
                {
                    SpawProjectile(Vector3.zero);
                }
            }

            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.gameObject.SetActive(true);
            }
        }

        public void StopFireWeapon()
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                particleSystem.gameObject.SetActive(false);
            }
        }
    }
}