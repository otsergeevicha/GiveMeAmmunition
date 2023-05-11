using UnityEngine;

namespace Services.Inputs
{
    public class InputService : IInputService
    {
        private readonly CustomPlayerInput _playerInput = new ();
        
        public void OnMoveControls() => 
            _playerInput.Player.Move.Enable();

        public void OffMoveControls() =>
            _playerInput.Player.Move.Disable();

        public Vector2 Axis => 
            _playerInput.Player.Move.ReadValue<Vector2>();
    }
}  