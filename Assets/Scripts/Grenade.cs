using EnemyLogic;
using Plugins.MonoCache;
using UnityEngine;

public class Grenade : MonoCache
{
    [SerializeField] private Transform _explosionEffect;

    private Collider[] _overlappedColliders = new Collider[30];

    private void OnTriggerEnter(Collider _)
    {
        _overlappedColliders = Physics.OverlapSphere(transform.position, Constants.RadiusExplosion);

        for (int i = 0; i < _overlappedColliders.Length; i++)
        {
            if (_overlappedColliders[i].gameObject.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(Constants.GrenadeDamage);

            if (_overlappedColliders[i].gameObject.TryGetComponent(out Rigidbody touchedExplosion))
                touchedExplosion.AddExplosionForce(Constants.ForceExplosion, transform.position, Constants.RadiusExplosion);
        }

        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}