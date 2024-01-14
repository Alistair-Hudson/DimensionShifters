using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Weapons
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
    public class GenericWeaponData : ScriptableObject
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
        private GenericWeapon _weaponPrefab;
        [SerializeField]
        private AnimatorOverrideController _weaponAnimations = null;
        [SerializeField]
        private int _weaponDamage = 1;
        [SerializeField]
        private bool _isMelee = false;
        [SerializeField]
        private Projectile _projectilePrefab = null;
        [SerializeField]
        private int _firingRate = 1;

        [field: SerializeField]
        public bool IsDualWeild { get; private set; } = false;

        private ProjectileSpawnPoint _projectileSpawnPoint = null;
        private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();

        public GenericWeapon Setup(Transform spawnPoint, Animator animator)
        {
            if (!_weaponPrefab)
            {
                Debug.LogError("No Weapon Prefab");
            }

            GenericWeapon newWeapon = Instantiate(_weaponPrefab, spawnPoint);
            newWeapon.SetUp(_projectilePrefab, _weaponDamage);
            if (_isMelee)
            {
                newWeapon.GetComponentInChildren<Collider>().gameObject.AddComponent<MeleeWeapon>().Damage = _weaponDamage;
            }

            
            if (_weaponAnimations)
            {
                animator.runtimeAnimatorController = _weaponAnimations;
            }

            return newWeapon;
        }
    }
}