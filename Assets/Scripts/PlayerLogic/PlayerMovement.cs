using Cysharp.Threading.Tasks;
using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoCache
    {
        [SerializeField] private float _speed;
        
        private CharacterController _controller;
        private IInputService _inputService;

        private Vector3 _movementVector;
        private Camera _camera;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputService = ServiceRouter.Container.Single<IInputService>();
        }

        protected override void OnEnabled() => 
            _inputService.OnMoveControls();

        protected override void OnDisabled() => 
            _inputService.OffMoveControls();

        protected override void UpdateCached() => 
            BaseLogic();

        private void BaseLogic()
        {
            _movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                Vector2 movement = _inputService.Axis;
                _movementVector = new Vector3(movement.x, 0, movement.y);
                _controller.Move(_movementVector * (_speed * Time.deltaTime));

                transform.forward = _movementVector;
            }

            _movementVector += Physics.gravity;

            _controller.Move(_movementVector * (_speed * Time.deltaTime));
        }
    }
}