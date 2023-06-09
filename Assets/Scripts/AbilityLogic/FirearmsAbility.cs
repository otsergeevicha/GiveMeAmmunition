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

        public override void Cast() => 
            ImitationQueue(Constants.AutomaticQueue);

        private async void ImitationQueue(int automaticQueue)
        {
            while (automaticQueue != 0)
            {
                if (Physics.Raycast(SendRay(), out RaycastHit hit))
                    _pool.TryGetBullet().Shot(_firearms.GetSpawnPoint((int)TypeGun.OneGun), hit.point);
                
                automaticQueue--;

                await UniTask.Delay(Constants.DelayShots);
            }
        }
        
        private Ray SendRay() => 
            _camera.ScreenPointToRay(GetCenter());

        private Vector2 GetCenter() => 
            new (Screen.width / 2f, Screen.height / 2f);
    }
}