using PlayerLogic;
using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.Points
{
    public class TurretPoints : MonoCache
    {
        [SerializeField] private SpawnPointTurret[] _pointTurrets;

        public SpawnPointTurret[] Get() =>
            _pointTurrets;
    }
}