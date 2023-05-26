using UnityEngine;

public static class Constants
{
    public const int RateCurtain = 3;
    public const float Epsilon = .001f;
    public const float RateAlfaCurtain = .03f;

    public const string InitialScene = "Initial";
    public const string PlayerPath = "Player/Hero";
    public const string HudPath = "Canvases/Hud";

    public static int EnemyLayerMask = 1 << 6;
    
    public const float FOVRange = 6f;
    public const float EnemySpeed = 2f;
    public const float AttackRange = 1f;
    public const int DamageEnemy = 10;

    public static readonly int HeroIdleHash = Animator.StringToHash("Idle");
    public static readonly int HeroWalkHash = Animator.StringToHash("Walk");
    public static readonly int HeroRollHash = Animator.StringToHash("Roll");
    
    public static readonly int EnemyIdleHash = Animator.StringToHash("Idle");
    public static readonly int EnemyWalkHash = Animator.StringToHash("Walk");
    public static readonly int EnemyAttackHash = Animator.StringToHash("Attack");
    public static readonly int EnemyHitHash = Animator.StringToHash("Hit");
}