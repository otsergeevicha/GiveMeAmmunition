using System;
using Services.ServiceLocator;
using UnityEngine;

namespace Services.Inputs
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        void OnMove(Action onMove);
        void OffMove(Action onMove);
        Vector2 LookAxis { get; }
        void PushZoom(Action onZoom);
        void PushShoot(Action onShoot);
        void OffShoot(Action offShoot);
        void PushGrenade(Action onGrenade);
        void PushFirearms(Action onFirearms);
        void PushFlamethrower(Action onFlamethrower);
        void PushShield(Action onShield);
        bool IsCurrentDevice();
        void OnControls();
        void OffControls();
    }
}