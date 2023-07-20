using Plugins.MonoCache;
using UnityEngine;

namespace EnemyLogic
{
    public class BotInput : MonoCache, IBotInput
    {
        public Vector2 MovementInput { get; set; }
    }
}