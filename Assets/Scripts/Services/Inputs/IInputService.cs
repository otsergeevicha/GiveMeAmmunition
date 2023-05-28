using Services.ServiceLocator;
using UnityEngine;

namespace Services.Inputs
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 LookAxis { get; }
        void OnMoveControls();
        void OffMoveControls();
        bool IsCurrentDevice();
    }
}