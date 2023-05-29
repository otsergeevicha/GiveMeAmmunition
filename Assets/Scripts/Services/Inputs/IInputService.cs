using System;
using Services.ServiceLocator;
using UnityEngine;

namespace Services.Inputs
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 LookAxis { get; }
        void PushZoom(Action action);
        void PushShoot(Action action);
        void PushGrenade(Action action);
        void PushFirearms(Action action);
        void PushFlamethrower(Action action);
        void PushShield(Action action);
        bool IsCurrentDevice();
        void OnControls();
        void OffControls();
    }
}