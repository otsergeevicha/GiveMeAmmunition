using System;
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
        
        public void PushZoom(Action action) => 
            _input.Player.Zoom.performed += _ => action?.Invoke();

        public void PushShoot(Action action) => 
            _input.Player.Shoot.performed += _ => action?.Invoke();

        public void PushGrenade(Action action) => 
            _input.Player.Grenade.performed += _ => action?.Invoke();

        public void PushFirearms(Action action) => 
            _input.Player.Firearms.performed += _ => action?.Invoke();

        public void PushFlamethrower(Action action) => 
            _input.Player.Flamethrower.performed += _ => action?.Invoke();
        
        public void PushShield(Action action) => 
            _input.Player.Shield.performed += _ => action?.Invoke();

        public bool IsCurrentDevice() => 
            _input.KeyboardMouseScheme.name == Constants.KeyboardMouse;

        public void OnControls() => 
            _input.Player.Enable();

        public void OffControls() =>
            _input.Player.Disable();
    }
}