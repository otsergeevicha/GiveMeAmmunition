using System;
using Ammo.Ammunition;
using Ammo.Pools;
using Infrastructure;
using UnityEngine;

namespace AbilityLogic
{
    public class GrenadeAbility : Ability
    {
        [SerializeField] private Transform _spawnPointGrenade;

        private readonly float _ourGravity = Physics.gravity.y;
    
        private float _axisX;
        private float _axisY;
        
        private Vector3 _direction;
        private Pool _pool;

        public override int GetIndexAbility() => 
            (int)IndexAbility.Grenade;

        public void InjectPool(Pool pool) => 
            _pool = pool;

        public void SetDirection(Vector3 direction) => 
            _direction = direction;

        public override void Cast()
        {
            Vector3 fromTo = _direction - transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);
            
            _axisX = fromToXZ.magnitude;
            _axisY = fromTo.y;

            float angleInRadians = Constants.AngleInDegrees * MathF.PI / 180;
            float rootOfSpeed = (_ourGravity * _axisX * _axisX) / (2 * (_axisY - Mathf.Tan(angleInRadians) * _axisX) *
                                                                   Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float speed = Mathf.Sqrt(Mathf.Abs(rootOfSpeed));

            Grenade grenade = _pool.TryGetGrenade();
            grenade.transform.position = _spawnPointGrenade.position;
            grenade.Get<Rigidbody>().velocity = _spawnPointGrenade.forward * speed;
        }
    }
}