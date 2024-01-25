using System.Collections;
using UnityEngine;

namespace DimensionShifters.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _projectileSpeed = 5f;
        [SerializeField]
        private float _lifeTime = 5;

        public int Damage = 1;

        private void Awake()
        {
            transform.LookAt(Camera.main.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * _projectileSpeed;
            Destroy(gameObject, _lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Projectile>(out var projectile))
            {
                return;
            }
            if (collision.gameObject.TryGetComponent<Player.PlayerHealth>(out var player))
            {
                player.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}