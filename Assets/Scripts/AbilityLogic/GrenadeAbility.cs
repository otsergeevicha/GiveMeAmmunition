using System;
using AbilityLogic.Cartridges;
using Ammo.Ammunition;
using CameraLogic;
using Infrastructure;
using Infrastructure.Factory.Pools;
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
        private Camera _camera;
        private MagazineGrenade _magazine;

        public override int GetIndexAbility() => 
            (int)IndexAbility.Grenade;

        public void Construct(Pool pool, CameraFollow cameraFollow)
        {
            _pool = pool;
            _camera = cameraFollow.GetCameraMain();
            _magazine = new MagazineGrenade(Constants.GrenadeMagazineSize);
        }

        public override void Cast()
        {
            Ray ray = SendRay();

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (_magazine.Check())
                {
                    Vector3 fromTo = raycastHit.point - transform.position;
                    Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
            
                    _axisX = fromToXZ.magnitude;
                    _axisY = fromTo.y;

                    float angleInRadians = Constants.AngleInDegrees * MathF.PI / 180;
                    float rootOfSpeed = (_ourGravity * _axisX * _axisX) / (2 * (_axisY - Mathf.Tan(angleInRadians) * _axisX) *
                                                                           Mathf.Pow(Mathf.Cos(angleInRadians), 2));
                    float speed = Mathf.Sqrt(Mathf.Abs(rootOfSpeed));

                    Grenade grenade = _pool.TryGetGrenade();
                    grenade.gameObject.SetActive(true);
                    grenade.transform.position = _spawnPointGrenade.position;
                    grenade.transform.LookAt(raycastHit.point);
                    grenade.Get<Rigidbody>().velocity = _spawnPointGrenade.forward * speed;
                    
                    _magazine.Spend();
                }
                
                _magazine.Shortage();
            }
        }
        
        private Ray SendRay() => 
            _camera.ScreenPointToRay(GetCenter());

        private Vector2 GetCenter() => 
            new (Screen.width / 2f, Screen.height / 2f);
    }
}