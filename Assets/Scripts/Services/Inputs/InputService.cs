using System;
using Infrastructure;
using UnityEngine;

namespace Services.Inputs
{
    public class InputService : IInputService
    {
        private readonly MapInputs _input = new ();
        
        public Vector2 MoveAxis => 
            _input.Player.Move.ReadValue<Vector2>();

        public Vector2 LookAxis => 
            _input.Player.Look.ReadValue<Vector2>();
        
        public void PushZoom(Action onZoom) => 
            _input.Player.Zoom.performed += _ =>
                onZoom?.Invoke();

        public void PushShoot(Action onShoot) => 
            _input.Player.Shoot.performed += _ =>
                onShoot?.Invoke();

        public void PushGrenade(Action onGrenade) => 
            _input.Player.Grenade.performed += _ =>
                onGrenade?.Invoke();

        public void PushFirearms(Action onFirearms) => 
            _input.Player.Firearms.performed += _ =>
                onFirearms?.Invoke();

        public void PushFlamethrower(Action onFlamethrower) => 
            _input.Player.Flamethrower.performed += _ =>
                onFlamethrower?.Invoke();
        
        public void PushShield(Action onShield) => 
            _input.Player.Shield.performed += _ =>
                onShield?.Invoke();

        public bool IsCurrentDevice() => 
            _input.KeyboardMouseScheme.name == Constants.KeyboardMouse;

        public void OnControls() => 
            _input.Player.Enable();

        public void OffControls() =>
            _input.Player.Disable();
    }
}