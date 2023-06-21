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

        [SerializeField] private Camera _camera;

        private readonly bool _cursorLocked = true;
        private readonly float _topClamp = 70.0f;
        private readonly float _bottomClamp = -30.0f;
        private readonly float _cameraAngleOverride = 0.0f;

        private IInputService _input;
        private float _sensitivity = 1f;
        private Transform _following;

        private float _rotationVelocity;

        private float _targetYaw;
        private float _targetPitch;
        private bool _zoom;

        private void Awake()
        {
            _zoomFollow.gameObject.SetActive(false);
            _input = ServiceLocator.Container.Single<IInputService>();

            SetCursorState(_cursorLocked);
            _input.PushZoom(OnZoom);
        }

        protected override void LateUpdateCached() =>
            CameraRotation();

        public Camera GetCameraMain() =>
            _camera;

        public void InitFollowing(Transform following)
        {
            _following = following;

            _cameraFollow.Follow = following;
            _zoomFollow.Follow = following;
        }

        public void SetSensitivity(float sensitivity) =>
            _sensitivity = sensitivity;

        private void OnZoom()
        {
            if (_zoom)
            {
                _zoomFollow.gameObject.SetActive(false);
                _cameraFollow.gameObject.SetActive(true);
                _zoom = false;
            }
            else
            {
                _zoomFollow.gameObject.SetActive(true);
                _cameraFollow.gameObject.SetActive(false);
                _zoom = true;
            }
        }

        private void CameraRotation()
        {
            if (_input.LookAxis.sqrMagnitude > Single.Epsilon)
            {
                float deltaTimeMultiplier = _input.IsCurrentDevice() 
                    ? _sensitivity 
                    : Time.deltaTime;

                _targetYaw += _input.LookAxis.x * deltaTimeMultiplier;
                _targetPitch += _input.LookAxis.y * deltaTimeMultiplier;
            }

            _targetYaw = ClampAngle(_targetYaw, float.MinValue, float.MaxValue);
            _targetPitch = ClampAngle(_targetPitch, _bottomClamp, _topClamp);

            _following.rotation = Quaternion.Euler(_targetPitch + _cameraAngleOverride, _targetYaw, 0.0f);
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