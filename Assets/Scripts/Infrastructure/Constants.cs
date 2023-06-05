using UnityEngine;

namespace Infrastructure
{
    public static class Constants
    {
        //hero
        public const int HeroSpeed = 4;
    
        //enemy
        public const int EnemyDamage = 10;
    
        //Firearms
        public const int AutomaticQueue = 3;
        public const int DelayShots = 100;
    
        //grenade
        public const float RadiusExplosion = 10f;
        public const float ForceExplosion = 500f;
        public const float AngleInDegrees = 45f;
        public const int GrenadeDamage = 50;
        
        //shield
        public const int TimeLifeShield = 5000;
    
        //bullet
        public const int BulletDamage = 50;
        public const float BulletSpeed = 20f;

        //pool
        public const int CountSpawnGrenade = 20;
        public const int CountSpawnBullets = 90;
    
        //curtain
        public const int RateCurtain = 3;
        public const float RateAlfaCurtain = .03f;

        //path loaded
        public const string InitialScene = "Initial";
        public const string MainScene = "Main";
        public const string PlayerPath = "Player/Hero";
        public const string HudPath = "Canvases/Hud";
        public const string CameraPath = "Camera/MainCamera";
        public const string LightPath = "View/Light";
        public const string PoolPath = "Ammo/Pool";
        public const string GrenadePath = "Ammo/Grenade";
        public const string BulletPath = "Ammo/Bullet";
    
        //check device
        public const string KeyboardMouse = "KeyboardMouse";

        //animator
        public static readonly int HeroWalkHash = Animator.StringToHash("Walk");
        public static readonly int HeroRollHash = Animator.StringToHash("Roll");
    }
}