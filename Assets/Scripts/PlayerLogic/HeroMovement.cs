using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : Hero
    {
        [SerializeField] private float _speed = 4f;
        
        private CharacterController _controller;
        private IInputService _inputService;

        private Camera _camera;
        private Animator _animator;

        private Animation _animation;

        private void Awake()
        {
            _controller = Get<CharacterController>();
            _inputService = ServiceRouter.Container.Single<IInputService>();
            _camera = Camera.main;
            _animator = Get<Animator>();
        }

        protected override void OnDisabled() =>
            _inputService.OffMoveControls();

        protected override void UpdateCached() =>
            BaseLogic();

        private void OnAnimEnded() => 
            _inputService.OnMoveControls();

        private void BaseLogic()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                _animator.SetBool(IsLoadedCargo ? Constants.WalkHash : Constants.RollHash, true);

                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }
            else
                _animator.SetBool(IsLoadedCargo ? Constants.WalkHash : Constants.RollHash, false);

            movementVector += Physics.gravity;

            _controller.Move(movementVector * (_speed * Time.deltaTime));
        }
    }
}