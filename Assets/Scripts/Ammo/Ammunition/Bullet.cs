using EnemyLogic;
using Infrastructure;
using Plugins.MonoCache;
using UnityEngine;

namespace Ammo.Ammunition
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoCache
    {
        [SerializeField] private Transform _vfxHitGreen;
        [SerializeField] private Transform _vfxHitRed;
    
        private Rigidbody _rigidbody;
        private Vector3 _firstPosition;

        private void Awake() => 
            _rigidbody = Get<Rigidbody>();

        public void Shot(Vector3 currentPosition, Vector3 direction)
        {
            transform.position = currentPosition;
            transform.LookAt(direction);
            gameObject.SetActive(true);
            
            _rigidbody.velocity = transform.forward * Constants.BulletSpeed;
        }

        private void OnTriggerEnter(Collider hit)
        {
            if (hit.gameObject.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(Constants.BulletDamage);

            Instantiate(hit.GetComponent<Enemy>() != null
                    ? _vfxHitGreen
                    : _vfxHitRed, transform.position,
                Quaternion.identity);
            
            gameObject.SetActive(false);
        }
    }
}