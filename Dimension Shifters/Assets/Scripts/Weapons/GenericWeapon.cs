using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DimensionShifters.Weapons
{
    public class GenericWeapon : MonoBehaviour
    {
        private Projectile _projectilePrefab = null;
        private ProjectileSpawnPoint _projectileSpawnPoint = null;

        private int _weaponDamage = 1;

        private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

        public AudioClip Sound { get; private set; } = null;

        public void SetUp(Projectile projectilePrefab, AudioClip sound, int weaponDamage)
        {
            _projectilePrefab = projectilePrefab;
            _projectileSpawnPoint = GetComponentInChildren<ProjectileSpawnPoint>();
            Sound = sound;

            _weaponDamage = weaponDamage;

            _particleSystems = GetComponentsInChildren<ParticleSystem>(true).ToList();
        }

        public void FireWeapon()
        {
            if (_projectilePrefab)
            {
                var newProjectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.transform);
                newProjectile.Damage = _weaponDamage;
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