using DimensionShifters.Player;
using DimensionShifters.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DimensionShifters.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private Transform _rightHand = null;
        [SerializeField]
        private Transform _leftHand = null;


        private Camera _player = null;
        private Animator _animator = null;
        private NavMeshAgent _navMesh = null;
        private AudioSource _audioSource = null;
        private List<GenericWeapon> _weapons = new List<GenericWeapon>();

        private float _attackRange = 2f;
        private float _runningSpeed = 1f;

        private bool _canAttackAgain = true;

        public void Setup(WorldEnemyList.EnemyData enemyData)
        {
            _player = Camera.main;
            _animator = GetComponent<Animator>();
            _navMesh = GetComponent<NavMeshAgent>();
            _navMesh.speed = _runningSpeed;
            _audioSource = GetComponent<AudioSource>();

            _weapons.Add(enemyData.Weapon.Setup(_rightHand, _animator));
            if (enemyData.Weapon.IsDualWeild)
            {
                _weapons.Add(enemyData.Weapon.Setup(_leftHand, _animator));
            }
            _attackRange = enemyData.AtackRange;
            _runningSpeed = enemyData.MaxRunningSpeed;

            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.Setup(enemyData.EnemyHealth, enemyData.PointsValue);
        }

        private void Update()
        {
            Vector3 myPlace = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 playerPlacement = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            if (Vector3.Distance(myPlace, playerPlacement) <= _attackRange)
            {
                MoveTowards(myPlace);
                if (!_canAttackAgain)
                {
                    return;
                }
                Attack();
            }
            else
            {
                MoveTowards(playerPlacement);
            }
        }

        private void Attack()
        {
            _canAttackAgain = false;
            Debug.Log("Attacking Player");
            transform.LookAt(_player.transform);
            _animator.SetTrigger("Attack");
        }

        private void MoveTowards(Vector3 destination)
        {
            UpdateAnimator();
            _navMesh.SetDestination(destination);
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMesh.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);
            float speed = localVel.z;
            _animator.SetFloat("ForwardSpeed", speed);
        }

        public void FootR()
        {

        }

        public void FootL()
        {

        }

        public void Hit()
        {
            _canAttackAgain = true;
        }

        public void Shoot()
        {
            foreach (GenericWeapon weapon in _weapons)
            {
                weapon.FireWeapon();
                _audioSource.PlayOneShot(weapon.Sound);
            }
        }

        public void ResetAttack()
        {
            foreach (GenericWeapon weapon in _weapons)
            {
                weapon.StopFireWeapon();
            }
            _canAttackAgain = true;
        }
    }
}