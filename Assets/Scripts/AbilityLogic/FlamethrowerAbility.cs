using CameraLogic;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace AbilityLogic
{
    public class FlamethrowerAbility : Ability
    {
        [SerializeField] private Transform _spawnPointBullet;
        [SerializeField] private Transform _vfxFlamethrower;
        
        private Camera _camera;
        private IInputService _input;
        private bool _isBurning;
        private Transform _vfx;
        private Ray _ray;

        private void Awake()
        {
            _input = ServiceLocator.Container.Single<IInputService>();
            _input.OffShoot(OffShoot);
        }

        private void OffShoot()
        {
            _isBurning = false;
            
            if (_vfx != null) 
                Destroy(_vfx.gameObject);
        }

        protected override void UpdateCached()
        {
            if (_isBurning == false)
                return;

            _ray = SendRay();

            if (Physics.Raycast(_ray, out RaycastHit raycastHit)) 
                _vfx.LookAt(raycastHit.point);
        }

        public override int GetIndexAbility() =>
            (int)IndexAbility.Flamethrower;

        public void Inject(CameraFollow cameraFollow) => 
            _camera = cameraFollow.GetCameraMain();

        public override void Cast()
        {
            _isBurning = true;
            _vfx = Instantiate(_vfxFlamethrower, _spawnPointBullet.position, Quaternion.identity);
        }
        
        private Ray SendRay() => 
            _camera.ScreenPointToRay(GetCenter());

        private Vector2 GetCenter() => 
            new (Screen.width / 2f, Screen.height / 2f);
    }
}