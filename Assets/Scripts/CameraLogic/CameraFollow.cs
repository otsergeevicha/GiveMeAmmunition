using System;
using Cinemachine;
using Plugins.MonoCache;
using Services.Inputs;
using Services.ServiceLocator;
using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoCache
    {
        [SerializeField] private CinemachineVirtualCamera _cameraFollow;
        [SerializeField] private CinemachineVirtualCamera _zoomFollow;
        
        private bool _cursorLocked;
        
        private readonly float _topClamp = 70.0f;
        private readonly float _bottomClamp = -30.0f;
        private readonly float _cameraAngleOverride = 0.0f;
        
        private IInputService _input;
        private float _sensitivity = 1f;
        private Transform _following;
        
        private float _rotationSmoothTime = 0.12f;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        
        private bool _isRotate = true;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private void Awake()
        {
            _zoomFollow.gameObject.SetActive(false);
            _input = ServiceRouter.Container.Single<IInputService>();
            
            SetCursorState(_cursorLocked);
        }

        protected override void LateUpdateCached() => 
            CameraRotation();

        public void InitFollowing(Transform following)
        {
            _following = following;
            
            _cameraFollow.Follow = following;
            _zoomFollow.Follow = following;
        }

        public void SetSensitivity(float sensitivity) => 
            _sensitivity = sensitivity;

        public void SetRotateOnMove(bool isRotate) => 
            _isRotate = isRotate;

        private void CameraRotation()
        {
            if (_input.LookAxis.sqrMagnitude > Single.Epsilon)
            {
                float deltaTimeMultiplier = _input.IsCurrentDevice() ? _sensitivity : Time.deltaTime;

                _cinemachineTargetYaw += _input.LookAxis.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.LookAxis.y * deltaTimeMultiplier;
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            _following.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) 
                lfAngle += 360f;
            if (lfAngle > 360f) 
                lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        
        private void SetCursorState(bool newState) => 
            Cursor.lockState = newState 
                ? CursorLockMode.Locked 
                : CursorLockMode.None;
    }
}