using Plugins.MonoCache;
using UnityEngine;

namespace EnemyLogic.EnemyAI.MoveState
{
    public class BotInput : MonoCache, IBotInput
    {
        public Vector2 MovementInput { get; set; }
    }
}