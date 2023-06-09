using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Weapons
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
    public class GenericWeapon : ScriptableObject
    {
        public class MeleeWeapon : MonoBehaviour
        {
            public int Damage = 1;

            private void OnTriggerEnter(Collider other)
            {
                if (other.TryGetComponent<Player.PlayerHealth>(out var player))
                {
                    player.TakeDamage(Damage);
                }
            }
        }

        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private AnimatorOverrideController _weaponAnimations = null;
        [SerializeField]
        private AudioClip _sound = null;
        public AudioClip Sound { get => _sound; }
        [SerializeField]
        private int _weaponDamage = 1;
        [SerializeField]
        private bool _isMelee = false;
        [SerializeField]
        private Projectile _projectilePrefab = null;
        [SerializeField]
        private int _firingRate = 1;

        private ParticleSystem _particleSystem = null;

        public void Setup(Transform spawnPoint, Animator animator)
        {
            if (!_prefab)
            {
                _particleSystem = spawnPoint.GetComponentInChildren<ParticleSystem>();
            }
            else
            {
                var newWeapon = Instantiate(_prefab, spawnPoint);

                if (_isMelee)
                {
                    newWeapon.GetComponentInChildren<Collider>().gameObject.AddComponent<MeleeWeapon>().Damage = _weaponDamage;
                }
                else if (!_projectilePrefab)
                {
                    _particleSystem = newWeapon.GetComponentInChildren<ParticleSystem>();
                    var em = _particleSystem.emission;
                    em.rateOverTime = _firingRate;
                }
            }
            
            if (_weaponAnimations)
            {
                animator.runtimeAnimatorController = _weaponAnimations;
            }
        }

        public void FireWeapon(Transform spawnPoint)
        {
            if (_projectilePrefab)
            {
                var newProjectile = Instantiate(_projectilePrefab, spawnPoint.position, Quaternion.identity);
                newProjectile.Damage = _weaponDamage;
            }
            else
            {
                _particleSystem.Emit(_firingRate);
            }
        }
    }
}