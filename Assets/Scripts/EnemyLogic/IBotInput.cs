using UnityEngine;

namespace EnemyLogic
{
    public interface IBotInput
    {
        Vector2 MovementInput { get; }
    }
}