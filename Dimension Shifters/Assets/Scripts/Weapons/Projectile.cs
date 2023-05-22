using UnityEngine;

namespace DimensionShifters.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _projectileSpeed = 5f;

        public int Damage = 1;

        private void Awake()
        {
            transform.LookAt(Camera.main.transform);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.PlayerHealth>(out var player))
            {
                player.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}