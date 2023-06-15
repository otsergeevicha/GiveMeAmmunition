using System;
using Infrastructure;
using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace PlayerLogic.Movements
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Hero))]
    public class HeroMovement : MonoCache
    {
        private CharacterController _controller;
        private IInputService _input;
        private Animator _animator;
        private float _rotationVelocity;
        private Hero _hero;

        private void Awake()
        {
            _controller = Get<CharacterController>();
            _input = ServiceLocator.Container.Single<IInputService>();
            _animator = Get<Animator>();
            _hero = Get<Hero>();
        }

        protected override void UpdateCached() =>
            BaseLogic();

        protected override void OnDisabled() =>
            _input.OffControls();

        private void OnAnimEnded() =>
            _input.OnControls();

        private void BaseLogic()
        {
            Vector3 movementVector = Vector3.zero;

            if (_input.MoveAxis.sqrMagnitude > Single.Epsilon)
            {
                _animator.SetBool(_hero.IsLoaded()
                    ? Constants.HeroWalkHash
                    : Constants.HeroRollHash, true);

                movementVector = new Vector3(_input.MoveAxis.x, 0.0f, _input.MoveAxis.y).normalized;

                transform.forward = movementVector;
            }
            else
            {
                _animator.SetBool(Constants.HeroWalkHash, false);
                _animator.SetBool(Constants.HeroRollHash, false);
            }

            movementVector += Physics.gravity;

            _controller.Move(movementVector * (Constants.HeroSpeed * Time.deltaTime));
        }
    }
}