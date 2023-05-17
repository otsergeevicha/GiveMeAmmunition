using UnityEngine;

public static class Constants
{
    public const int RateCurtain = 3;
    public const float Epsilon = .001f;
    public const float RateAlfaCurtain = .03f;
    public const float MinDistanceToTarget = 1.5f;
    public const string InitialScene = "Initial";
    public const string PlayerPath = "Player/Hero";
    public const string HudPath = "Canvases/Hud";

    public static readonly int IdleHash = Animator.StringToHash("Idle");
    public static readonly int WalkHash = Animator.StringToHash("Walk");
    public static readonly int RollHash = Animator.StringToHash("Roll");
}