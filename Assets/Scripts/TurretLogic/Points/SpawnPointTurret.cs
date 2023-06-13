using Plugins.MonoCache;
using UnityEngine;

namespace TurretLogic.Points
{
    public class SpawnPointTurret : MonoCache
    {
        public Vector3 GetPosition() => 
            transform.position;
    }
}