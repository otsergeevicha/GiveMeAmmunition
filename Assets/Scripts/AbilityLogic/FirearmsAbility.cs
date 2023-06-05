using Ammo.FirearmsGun;
using Ammo.Pools;
using CameraLogic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;

namespace AbilityLogic
{
    public class FirearmsAbility : Ability
    {
        [SerializeField] private Firearms _firearms;

        private Pool _pool;
        private Camera _camera;

        public override int GetIndexAbility() =>
            (int)IndexAbility.Firearms;

        public void Inject(Pool pool, CameraFollow cameraFollow)
        {
            _pool = pool;
            _camera = cameraFollow.GetCameraMain();
        }

        public override void Cast()
        {
            Ray ray = SendRay();

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
                ImitationQueue(Constants.AutomaticQueue, raycastHit.point);
        }

        private Ray SendRay() => 
            _camera.ScreenPointToRay(GetCenter());

        private Vector2 GetCenter() => 
            new Vector2(Screen.width / 2f, Screen.height / 2f);

        private async void ImitationQueue(int automaticQueue, Vector3 mouseWorldPosition)
        {
            while (automaticQueue != 0)
            {
                _pool.TryGetBullet().Shot(_firearms.GetSpawnPoint((int)TypeGun.OneGun), mouseWorldPosition);
                automaticQueue--;

                await UniTask.Delay(Constants.DelayShots);
            }
        }
    }
}