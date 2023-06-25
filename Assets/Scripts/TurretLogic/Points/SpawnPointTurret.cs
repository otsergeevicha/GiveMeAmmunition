using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.Points
{
    [RequireComponent(typeof(TurretPointTrigger))]
    public class SpawnPointTurret : MonoCache
    {
        public Transform GetPosition() =>
            transform;
    }
}