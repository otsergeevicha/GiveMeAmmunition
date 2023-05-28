using System;
using CameraLogic;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Hero))]
    public class HeroMovement : Hero
    {
        private CharacterController _controller;
        private IInputService _input;
        private Animator _animator;

        private void Awake()
        {
            _controller = Get<CharacterController>();
            _input = ServiceRouter.Container.Single<IInputService>();
            _animator = Get<Animator>();
        }

        protected override void UpdateCached() =>
            BaseLogic();

        protected override void OnDisabled() =>
            _input.OffMoveControls();

        private void OnAnimEnded() => 
            _input.OnMoveControls();

        private void BaseLogic()
        {
            Vector3 movementVector = Vector3.zero;
            
            if (_input.MoveAxis.sqrMagnitude > Single.Epsilon)
            {
                _animator.SetBool(IsLoadedCargo 
                    ? Constants.HeroWalkHash 
                    : Constants.HeroRollHash, true);
            
                movementVector = new Vector3(_input.MoveAxis.x, 0.0f, _input.MoveAxis.y).normalized;

                transform.forward = movementVector;
            }
            else
                _animator.SetBool(IsLoadedCargo 
                    ? Constants.HeroWalkHash 
                    : Constants.HeroRollHash, false);
            
            movementVector += Physics.gravity;
            
            _controller.Move(movementVector * (Constants.HeroSpeed * Time.deltaTime));
        }
    }
}