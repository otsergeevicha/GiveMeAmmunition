using UnityEngine;

namespace Services.Inputs
{
    public class InputService : IInputService
    {
        private readonly MapInputs _input = new ();

        public bool IsCurrentDevice() => 
            _input.KeyboardMouseScheme.name == Constants.KeyboardMouse;

        public Vector2 MoveAxis => 
            _input.Player.Move.ReadValue<Vector2>();

        public Vector2 LookAxis => 
            _input.Player.Look.ReadValue<Vector2>();

        public void OnControls() => 
            _input.Player.Enable();

        public void OffControls() =>
            _input.Player.Disable();
    }
}