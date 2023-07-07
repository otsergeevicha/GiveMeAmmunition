using Plugins.MonoCache;
using Services.Health;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoCache, IHealth
    {
        public void TakeDamage(int damage) {}

        public void OnActive(Vector3 newPosition)
        {
            gameObject.SetActive(true);
            transform.position = newPosition;
        }
    }
}