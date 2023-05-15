using System;
using Plugins.MonoCache;
using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoCache
    {
        [SerializeField] private Transform _following;

        [SerializeField] private float _distance;

        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _rotationAngleY;
        [SerializeField] private float _rotationAngleZ;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;
        
        private Transform _cameraTransform;
        
        private void Start() => 
            _cameraTransform = transform;

        protected override void LateUpdateCached() => 
            BaseLogic();

        public void Follow(GameObject following) =>
            _following = following.transform;

        private void BaseLogic()
        {
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, _rotationAngleZ);

            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPosition();

            _cameraTransform.rotation = rotation;
            _cameraTransform.position = position;
        }

        private Vector3 FollowingPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetX;
            followingPosition.y += _offsetY;
            followingPosition.y += _offsetZ;
            return followingPosition;
        }
    }
}