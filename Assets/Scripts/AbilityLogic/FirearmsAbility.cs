using System.Threading;
using AbilityLogic.Cartridges;
using Ammo.FirearmsGun;
using Ammo.Pools;
using CameraLogic;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace AbilityLogic
{
    public class FirearmsAbility : Ability
    {
        [SerializeField] private Firearms _firearms;

        private readonly CancellationTokenSource _shootToken = new ();
        
        private Pool _pool;
        private Camera _camera;
        private bool _isAttack;
        private MagazineFirearms _magazine;

        private void Awake()
        {
            IInputService input = ServiceLocator.Container.Single<IInputService>();
            input.OffShoot(OffShoot);
            _magazine = new MagazineFirearms(Constants.FirearmsMagazineSize);
        }

        public override int GetIndexAbility() =>
            (int)IndexAbility.Firearms;

        public void Inject(Pool pool, CameraFollow cameraFollow)
        {
            _pool = pool;
            _camera = cameraFollow.GetCameraMain();
        }

        public override void Cast()
        {
            _isAttack = true;
            ImitationQueue();
        }

        private void OffShoot()
        {
            _isAttack = false;
            _shootToken.Cancel();
        }

        private async void ImitationQueue()
        {
            while (_isAttack)
            {
                if (Physics.Raycast(SendRay(), out RaycastHit hit))
                {
                    if (_magazine.Check())
                    {
                        _pool.TryGetBullet().Shot(_firearms.GetSpawnPoint((int)TypeGun.OneGun), hit.point);
                        _magazine.Spend();
                    }
                    
                    _magazine.Shortage();
                }
                
                await UniTask.Delay(Constants.DelayShots);
            }
        }
        
        private Ray SendRay() => 
            _camera.ScreenPointToRay(GetCenter());

        private Vector2 GetCenter() => 
            new (Screen.width / 2f, Screen.height / 2f);
    }
}