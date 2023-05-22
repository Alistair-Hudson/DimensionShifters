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
        private int _weaponDamage = 1;
        [SerializeField]
        private bool _isMelee = false;

        public void Setup(Transform spawnPoint, Animator animator)
        {
            var newWeapon = Instantiate(_prefab, spawnPoint);
            if (_weaponAnimations)
            {
                animator.runtimeAnimatorController = _weaponAnimations;
            }

            if (_isMelee)
            {
                newWeapon.AddComponent<MeleeWeapon>().Damage = _weaponDamage;
            }
        }

        public void FireWeapon()
        {

        }
    }
}