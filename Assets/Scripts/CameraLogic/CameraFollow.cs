using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _following;

        [SerializeField] private float _distance;

        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _rotationAngleY;
        [SerializeField] private float _rotationAngleZ;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;

        private void Start() => 
            LateRun();

        private async void LateRun()
        {
            while (_following != null)
            {
                BaseLogic();
                
                await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);
            }
        }

        public void Follow(GameObject following) =>
            _following = following.transform;

        private void BaseLogic()
        {
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, _rotationAngleZ);

            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPosition();

            transform.rotation = rotation;
            transform.position = position;
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