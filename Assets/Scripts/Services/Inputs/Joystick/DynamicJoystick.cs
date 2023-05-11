using UnityEngine;

namespace Services.Inputs.Joystick
{
    public class DynamicJoystick : Joystick
    {
        [SerializeField] private bool _hideOnPointerUp;
        [SerializeField] private bool _centralizeOnPointerUp = true;

        protected override void Awake()
        {
            JoystickType = VirtualJoystickType.Floating;
            HideOnPointerUp = _hideOnPointerUp;
            CentralizeOnPointerUp = _centralizeOnPointerUp;

            base.Awake();
        }
    }
}