using Cysharp.Threading.Tasks;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private bool _isAlive;
        
        private CharacterController _controller;
        private IInputService _inputService;

        private Vector3 _movementVector;
        private Camera _camera;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputService = ServiceRouter.Container.Single<IInputService>();
        }

        private void Start()
        {
            if (_controller != null)
                Run();
        }

        private void OnEnable()
        {
            _isAlive = true;
            _inputService.OnMoveControls();
        }

        private void OnDisable()
        {
            _isAlive = false;
            _inputService.OffMoveControls();
        }

        private async void Run()
        {
            while (_isAlive)
            {
                BaseLogic();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

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