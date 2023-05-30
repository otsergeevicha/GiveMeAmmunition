using CameraLogic;
using UnityEngine;

namespace AbilityLogic
{
    public class FlamethrowerAbility : Ability
    {
        [SerializeField] private Transform _spawnPointBullet;
        private Camera _camera;

        public override int GetIndexAbility() =>
            (int)IndexAbility.Flamethrower;

        public void Inject(CameraFollow cameraFollow) => 
            _camera = cameraFollow.GetCameraMain();

        public override void Cast()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = _camera.ScreenPointToRay(screenCenterPoint);

            // if (Physics.Raycast(ray, out RaycastHit raycastHit)) 
            //     ImitationQueue(Constants.AutomaticQueue, raycastHit.point);
        }
    }
}