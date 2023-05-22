using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Weapons
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
    public class GenericWeapon : ScriptableObject
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private AnimatorOverrideController _weaponAnimations = null;

        public void Setup(Transform spawnPoint, Animator animator)
        {
            var newWeapon = Instantiate(_prefab, spawnPoint);
            if (_weaponAnimations)
            {
                animator.runtimeAnimatorController = _weaponAnimations;
            }
        }

        public void FireWeapon()
        {

        }
    }
}