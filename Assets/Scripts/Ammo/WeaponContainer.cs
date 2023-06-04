using AbilityLogic;
using CameraLogic;
using PlayerLogic;
using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace Ammo
{
    public class WeaponContainer : MonoCache
    {
        [SerializeField] private FirearmsAbility _firearms;
        [SerializeField] private FlamethrowerAbility _flamethrower;
        [SerializeField] private GrenadeAbility _grenade;

        private Hero _hero;
        private IInputService _input;
        private Camera _camera;
        private bool _zoom;

        private void Awake()
        {
            _input = ServiceLocator.Container.Single<IInputService>();
            _hero = ParentGet<Hero>();
            _input.OnMove(OnMove);
            _input.OffMove(OffMove);
        }

        private void Update()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = _camera.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _firearms.gameObject.transform.LookAt(raycastHit.point);
                _flamethrower.gameObject.transform.LookAt(raycastHit.point);
                _grenade.gameObject.transform.LookAt(raycastHit.point);
            }
        }

        public void Inject(CameraFollow cameraFollow) =>
            _camera = cameraFollow.GetCameraMain();

        private void OnMove() =>
            gameObject.SetActive(_hero.IsLoaded());

        private void OffMove() =>
            gameObject.SetActive(true);
    }
}