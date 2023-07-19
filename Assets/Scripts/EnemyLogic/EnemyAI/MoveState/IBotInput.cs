using UnityEngine;

namespace EnemyLogic.EnemyAI.MoveState
{
    public interface IBotInput
    {
        Vector2 MovementInput { get; }
    }
}