using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic
{
    public class TurretPoints : MonoCache
    {
        [SerializeField] private SpawnPointTurret[] _pointTurrets;

        public SpawnPointTurret[] Get() =>
            _pointTurrets;
    }
}