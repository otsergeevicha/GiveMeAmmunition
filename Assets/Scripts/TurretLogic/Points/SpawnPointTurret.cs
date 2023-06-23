using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.Points
{
    public class SpawnPointTurret : MonoCache
    {
        [SerializeField] private Transform _vfxFreePlace;

        private Turret _turret;
        private Transform _whiteCircle;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Hero _))
            {
                if (_turret.Purchased)
                    _turret.TryUpgrade();
                else
                    _turret.Purchase(_whiteCircle);
            }
        }

        public Transform GetPosition() =>
            transform;

        public void SetTurret(Turret turret)
        {
            _turret = turret;

            if (!_turret.Purchased)
               _whiteCircle = Instantiate(_vfxFreePlace, transform.position, Quaternion.identity);
        }
    }
}